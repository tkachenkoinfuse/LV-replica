using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ManualTimeImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ManualTime Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.manual_time.Max(e => e.record_id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ManualTimeImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("ManualTime Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ManualTime> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ManualTimeImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ManualTime> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ManualTime> manualTimeList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.manual_time.Find(table.record_id);
                    if (existing != null)
                    {
                        _baseContextTarget.manual_time.Remove(existing);
                    }
                    manualTimeList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.manual_time.AddRange(manualTimeList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ManualTime> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ManualTime> manualTime = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.manual_time
                    .Where(t => (t.record_id >= idStart && t.record_id <= idEnd))
                    .OrderBy(t => (t.record_id));
                foreach (var data in tableData)
                {
                    manualTime.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return manualTime;
        }
    }
}
