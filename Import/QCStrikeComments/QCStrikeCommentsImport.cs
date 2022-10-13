using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class QCStrikeCommentsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("QCStrikeComments Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.qc_strike_comments.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<QCStrikeCommentsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("QCStrikeComments Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<QCStrikeComments> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<QCStrikeCommentsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<QCStrikeComments> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<QCStrikeComments> qcStrikeCommentsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.qc_strike_comments.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.qc_strike_comments.Remove(existing);
                    }
                    qcStrikeCommentsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.qc_strike_comments.AddRange(qcStrikeCommentsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<QCStrikeComments> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<QCStrikeComments> qcStrikeComments = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.qc_strike_comments
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    qcStrikeComments.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return qcStrikeComments;
        }
    }
}
