using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class QCCheckReactionsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("QCCheckReactions Start!");
            DateTime dateEnd = DateTime.Now;
            DateTime dateStart = dateEnd;
            DateTime deadline = dateEnd.AddDays(-1 * intervalDays);
            int i = 0;
            do
            {
                Console.WriteLine($"i is: {i}.");
                if (i > 0)
                    dateEnd = dateStart;

                dateStart = dateEnd.AddHours(-24);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"dateStart is: {dateStart_}.");
                Console.WriteLine($"dateEnd is: {dateEnd_}.");
                BackgroundJob.Enqueue<QCCheckReactionsImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("QCCheckReactions Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<QCCheckReactions> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<QCCheckReactionsImport>(j => j.writeData(sourceData));
                start += 10000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<QCCheckReactions> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<QCCheckReactions> qcCheckReactionsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.qc_check_reactions.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.qc_check_reactions.Remove(existing);
                    }
                    qcCheckReactionsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.qc_check_reactions.AddRange(qcCheckReactionsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<QCCheckReactions> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<QCCheckReactions> qcCheckReactions = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.qc_check_reactions
                    .Where(t => (t.updated_at >= dateStart && t.updated_at < dateEnd))
                    .Skip(start)
                    .Take(10000)
                    .OrderBy(t => t.updated_at);
                foreach (var data in tableData)
                {
                    qcCheckReactions.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return qcCheckReactions;
        }
    }
}
