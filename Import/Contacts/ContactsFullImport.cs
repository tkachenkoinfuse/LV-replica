using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ContactFullImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("Contacts Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.contacts_view.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ContactFullImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("Contacts Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<Contacts> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ContactFullImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<Contacts> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<Contacts> contactList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.contacts.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.contacts.Remove(existing);
                    }
                    contactList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.contacts.AddRange(contactList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<Contacts> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<Contacts> contact = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.contacts_view
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    contact.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return contact;
        }
    }
}
