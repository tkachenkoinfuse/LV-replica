using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class QCStrikeReasonCommentImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("QCStrikeReasonComment Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.qc_strike_reason_comment.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<QCStrikeReasonCommentImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("QCStrikeReasonComment Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<QCStrikeReasonComment> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<QCStrikeReasonCommentImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<QCStrikeReasonComment> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<QCStrikeReasonComment> qcStrikeReasonCommentList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.qc_strike_reason_comment.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.qc_strike_reason_comment.Remove(existing);
                    }
                    qcStrikeReasonCommentList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.qc_strike_reason_comment.AddRange(qcStrikeReasonCommentList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<QCStrikeReasonComment> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<QCStrikeReasonComment> qcStrikeReasonComment = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.qc_strike_reason_comment
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    qcStrikeReasonComment.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return qcStrikeReasonComment;
        }
    }
}
