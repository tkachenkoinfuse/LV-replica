using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class QCStrikeVerdictsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("QCStrikeVerdicts Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.qc_strike_verdicts.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<QCStrikeVerdictsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("QCStrikeVerdicts Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<QCStrikeVerdicts> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<QCStrikeVerdictsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<QCStrikeVerdicts> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<QCStrikeVerdicts> qcStrikeVerdictsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.qc_strike_verdicts.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.qc_strike_verdicts.Remove(existing);
                    }
                    qcStrikeVerdictsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.qc_strike_verdicts.AddRange(qcStrikeVerdictsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<QCStrikeVerdicts> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<QCStrikeVerdicts> qcStrikeVerdicts = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.qc_strike_verdicts
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    qcStrikeVerdicts.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return qcStrikeVerdicts;
        }
    }
}
