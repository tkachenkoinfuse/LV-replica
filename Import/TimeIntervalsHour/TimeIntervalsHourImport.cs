using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class TimeIntervalsHourImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("TimeIntervalsHour Start!");
            DateTime dateEnd = DateTime.Now;
            DateTime dateStart = dateEnd;
            DateTime deadline = dateEnd.AddDays(-1 * intervalDays);
            int i = 0;
            do
            {
                if (i > 0)
                    dateEnd = dateStart;

                dateStart = dateEnd.AddDays(-1);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd");
                //Console.WriteLine($"dateStart is: {dateStart_}.");
                //Console.WriteLine($"dateEnd is: {dateEnd_}.");
                BackgroundJob.Enqueue<TimeIntervalsHourImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("TimeIntervalsHour Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<TimeIntervalsHour> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<TimeIntervalsHourImport>(j => j.writeData(sourceData));
                start += 10000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<TimeIntervalsHour> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<TimeIntervalsHour> timeIntervalsHourList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.time_intervals_hour.Find(table.user_id, table.day, table.hour);
                    if (existing != null)
                    {
                        _baseContextTarget.time_intervals_hour.Remove(existing);
                    }
                    timeIntervalsHourList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.time_intervals_hour.AddRange(timeIntervalsHourList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<TimeIntervalsHour> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<TimeIntervalsHour> timeIntervalsHour = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.time_intervals_hour
                    .Where(t => (t.user_id >0 && t.day >= dateStart && t.day < dateEnd))
                    .Skip(start)
                    .Take(10000)
                    .OrderBy(t => (t.user_id));
                foreach (var data in tableData)
                {
                    timeIntervalsHour.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return timeIntervalsHour;
        }
    }
}
