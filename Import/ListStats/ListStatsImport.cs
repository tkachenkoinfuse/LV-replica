using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ListStatsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ListStats Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.list_stats.Max(e => e.list_id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ListStatsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("ListStats Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ListStats> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ListStatsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ListStats> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ListStats> listStatsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.list_stats.Find(table.list_id);
                    if (existing != null)
                    {
                        _baseContextTarget.list_stats.Remove(existing);
                    }
                    listStatsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.list_stats.AddRange(listStatsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ListStats> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ListStats> listStats = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.list_stats
                    .Where(t => (t.list_id >= idStart && t.list_id <= idEnd))
                    .OrderBy(t => (t.list_id));
                foreach (var data in tableData)
                {
                    listStats.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return listStats;
        }
    }
}
