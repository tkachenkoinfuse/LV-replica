using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class UsersImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("Users Start!");
            DateTime dateEnd = DateTime.Now;
            DateTime dateStart = dateEnd;
            DateTime deadline = dateEnd.AddDays(-1 * intervalDays);
            int i = 0;
            do
            {
                if (i > 0)
                    dateEnd = dateStart;

                dateStart = dateEnd.AddHours(-24);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                //Console.WriteLine($"dateStart is: {dateStart_}.");
                //Console.WriteLine($"dateEnd is: {dateEnd_}.");
                BackgroundJob.Enqueue<UsersImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("Users Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<Users> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<UsersImport>(j => j.writeData(sourceData));
                start += 1000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<Users> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<Users> usersList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.users.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.users.Remove(existing);
                    }
                    usersList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.users.AddRange(usersList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<Users> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<Users> users = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.users
                    .Where(t => (t.updated_at >= dateStart && t.updated_at < dateEnd))
                    .Skip(start)
                    .Take(1000)
                    .OrderBy(t => t.updated_at);
                foreach (var data in tableData)
                {
                    users.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return users;
        }
    }
}
