using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ContactTitlesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ContactTitles Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.contact_titles.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ContactTitlesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("ContactTitles Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ContactTitles> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ContactTitlesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ContactTitles> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ContactTitles> contactTitlesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.contact_titles.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.contact_titles.Remove(existing);
                    }
                    contactTitlesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.contact_titles.AddRange(contactTitlesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ContactTitles> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ContactTitles> contactTitles = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.contact_titles
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    contactTitles.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return contactTitles;
        }
    }
}
