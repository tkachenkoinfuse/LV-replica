using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class RejectsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("Rejects Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.rejects.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<RejectsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("Rejects Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<Rejects> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<RejectsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<Rejects> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<Rejects> rejectsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.rejects.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.rejects.Remove(existing);
                    }
                    rejectsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.rejects.AddRange(rejectsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<Rejects> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<Rejects> rejects = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.rejects
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    rejects.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return rejects;
        }
    }
}
