using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class UserSchedulesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("UserSchedules Start!");
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
                BackgroundJob.Enqueue<UserSchedulesImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("UserSchedules Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<UserSchedules> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<UserSchedulesImport>(j => j.writeData(sourceData));
                start += 10000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<UserSchedules> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<UserSchedules> userSchedulesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.user_schedules.Find(table.user_id, table.day);
                    if (existing != null)
                    {
                        _baseContextTarget.user_schedules.Remove(existing);
                    }
                    userSchedulesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.user_schedules.AddRange(userSchedulesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<UserSchedules> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<UserSchedules> userSchedules = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.user_schedules
                    .Where(t => (t.user_id >0 && t.day >= dateStart && t.day < dateEnd))
                    .Skip(start)
                    .Take(10000)
                    .OrderBy(t => (t.user_id));
                foreach (var data in tableData)
                {
                    userSchedules.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return userSchedules;
        }
    }
}
