using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class IntervalsInScheduleImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("IntervalsInSchedule Start!");
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
                BackgroundJob.Enqueue<IntervalsInScheduleImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("IntervalsInSchedule Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<IntervalsInSchedule> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<IntervalsInScheduleImport>(j => j.writeData(sourceData));
                start += 10000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<IntervalsInSchedule> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<IntervalsInSchedule> intervalsInScheduleList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.intervals_in_schedule.Find(table.user_id, table.day, table.hour, table.type_id);
                    if (existing != null)
                    {
                        _baseContextTarget.intervals_in_schedule.Remove(existing);
                    }
                    intervalsInScheduleList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.intervals_in_schedule.AddRange(intervalsInScheduleList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<IntervalsInSchedule> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<IntervalsInSchedule> intervalsInSchedule = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.intervals_in_schedule
                    .Where(t => (t.user_id >0 && t.day >= dateStart && t.day < dateEnd))
                    .Skip(start)
                    .Take(10000)
                    .OrderBy(t => (t.user_id));
                foreach (var data in tableData)
                {
                    intervalsInSchedule.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return intervalsInSchedule;
        }
    }
}
