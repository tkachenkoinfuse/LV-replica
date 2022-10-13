using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ManualVerificationsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ManualVerifications Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.manual_verifications.Max(e => e.record_id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ManualVerificationsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("ManualVerifications Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ManualVerifications> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ManualVerificationsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ManualVerifications> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ManualVerifications> manualVerificationsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.manual_verifications.Find(table.record_id);
                    if (existing != null)
                    {
                        _baseContextTarget.manual_verifications.Remove(existing);
                    }
                    manualVerificationsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.manual_verifications.AddRange(manualVerificationsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ManualVerifications> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ManualVerifications> manualVerifications = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.manual_verifications
                    .Where(t => (t.record_id >= idStart && t.record_id <= idEnd))
                    .OrderBy(t => (t.record_id));
                foreach (var data in tableData)
                {
                    manualVerifications.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return manualVerifications;
        }
    }
}
