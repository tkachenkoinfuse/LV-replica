using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ListStatsNewCriteriasImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ListStatsNewCriterias Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.list_stats_new_criterias.Max(e => e.list_id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ListStatsNewCriteriasImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("ListStatsNewCriterias Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ListStatsNewCriterias> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ListStatsNewCriteriasImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ListStatsNewCriterias> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ListStatsNewCriterias> listStatsNewCriteriasList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.list_stats_new_criterias.Find(table.list_id);
                    if (existing != null)
                    {
                        _baseContextTarget.list_stats_new_criterias.Remove(existing);
                    }
                    listStatsNewCriteriasList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.list_stats_new_criterias.AddRange(listStatsNewCriteriasList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ListStatsNewCriterias> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ListStatsNewCriterias> listStatsNewCriterias = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.list_stats_new_criterias
                    .Where(t => (t.list_id >= idStart && t.list_id <= idEnd))
                    .OrderBy(t => (t.list_id));
                foreach (var data in tableData)
                {
                    listStatsNewCriterias.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return listStatsNewCriterias;
        }
    }
}
