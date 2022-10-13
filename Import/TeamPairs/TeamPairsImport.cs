using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class TeamPairsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("TeamPairs Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.team_pairs.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<TeamPairsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("TeamPairs Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<TeamPairs> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<TeamPairsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<TeamPairs> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<TeamPairs> teamPairsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.team_pairs.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.team_pairs.Remove(existing);
                    }
                    teamPairsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.team_pairs.AddRange(teamPairsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<TeamPairs> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<TeamPairs> teamPairs = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.team_pairs
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    teamPairs.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return teamPairs;
        }
    }
}
