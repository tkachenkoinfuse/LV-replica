using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class LSBatchStatusesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("LSBatchStatuses Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.l_s_batch_statuses.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<LSBatchStatusesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("LSBatchStatuses Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<LSBatchStatuses> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<LSBatchStatusesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<LSBatchStatuses> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<LSBatchStatuses> lsBatchStatusesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.l_s_batch_statuses.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.l_s_batch_statuses.Remove(existing);
                    }
                    lsBatchStatusesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.l_s_batch_statuses.AddRange(lsBatchStatusesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<LSBatchStatuses> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<LSBatchStatuses> lsBatchStatuses = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.l_s_batch_statuses
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    lsBatchStatuses.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return lsBatchStatuses;
        }
    }
}
