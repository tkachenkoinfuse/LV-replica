using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class OvStatusesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("OvStatuses Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.ov_statuses.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<OvStatusesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("OvStatuses Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<OvStatuses> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<OvStatusesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<OvStatuses> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<OvStatuses> ovStatusesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.ov_statuses.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.ov_statuses.Remove(existing);
                    }
                    ovStatusesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.ov_statuses.AddRange(ovStatusesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<OvStatuses> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<OvStatuses> ovStatuses = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.ov_statuses
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    ovStatuses.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return ovStatuses;
        }
    }
}
