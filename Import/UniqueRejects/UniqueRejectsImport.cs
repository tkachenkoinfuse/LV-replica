using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class UniqueRejectsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("UniqueRejects Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.unique_rejects.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<UniqueRejectsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("UniqueRejects Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<UniqueRejects> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<UniqueRejectsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<UniqueRejects> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<UniqueRejects> uniqueRejectsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.unique_rejects.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.unique_rejects.Remove(existing);
                    }
                    uniqueRejectsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.unique_rejects.AddRange(uniqueRejectsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<UniqueRejects> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<UniqueRejects> uniqueRejects = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.unique_rejects
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    uniqueRejects.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return uniqueRejects;
        }
    }
}
