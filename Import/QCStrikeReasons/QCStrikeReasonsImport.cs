using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class QCStrikeReasonsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("QCStrikeReasons Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.qc_strike_reasons.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<QCStrikeReasonsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("QCStrikeReasons Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<QCStrikeReasons> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<QCStrikeReasonsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<QCStrikeReasons> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<QCStrikeReasons> qcStrikeReasonsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.qc_strike_reasons.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.qc_strike_reasons.Remove(existing);
                    }
                    qcStrikeReasonsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.qc_strike_reasons.AddRange(qcStrikeReasonsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<QCStrikeReasons> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<QCStrikeReasons> qcStrikeReasons = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.qc_strike_reasons
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    qcStrikeReasons.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return qcStrikeReasons;
        }
    }
}
