using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ContactsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("Contacts Start!");
            DateTime dateEnd = DateTime.Now;
            DateTime dateStart = dateEnd;
            DateTime deadline = dateEnd.AddDays(-1 * intervalDays);
            int i = 0;
            do
            {
                if (i > 0)
                    dateEnd = dateStart;

                dateStart = dateEnd.AddHours(-1);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                //Console.WriteLine($"dateStart is: {dateStart_}.");
                BackgroundJob.Enqueue<ContactsImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);
           
            Log.Logger.Information("Contacts Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<Contacts> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<ContactsImport>(j => j.writeData(sourceData));
                start += 1000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<Contacts> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<Contacts> contactsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.contacts.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.contacts.Remove(existing);
                    }
                    //Log.Logger.Information($"{a}. Id is: {table.id}.");
                    contactsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.contacts.AddRange(contactsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<Contacts> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<Contacts> contacts = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.contacts_view
                    .Where(t => (t.updated_at >= dateStart && t.updated_at < dateEnd))
                    .Skip(start)
                    .Take(1000)
                    .OrderBy(t => t.updated_at);
                foreach (var data in tableData)
                {
                    contacts.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return contacts;
        }
    }
}
