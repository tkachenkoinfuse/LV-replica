using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class FeedLogImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("FeedLog Start!");
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
                BackgroundJob.Enqueue<FeedLogImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("FeedLog Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<FeedLog> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<FeedLogImport>(j => j.writeData(sourceData));
                start += 1000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<FeedLog> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<FeedLog> feedLogList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.feed_log.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.feed_log.Remove(existing);
                    }
                    feedLogList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.feed_log.AddRange(feedLogList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<FeedLog> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<FeedLog> feedLog = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.feed_log_view
                    .Where(t => (t.log_created_at >= dateStart && t.log_created_at < dateEnd))
                    .Skip(start)
                    .Take(1000)
                    .OrderBy(t => t.log_created_at);
                foreach (var data in tableData)
                {
                    feedLog.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return feedLog;
        }
    }
}
