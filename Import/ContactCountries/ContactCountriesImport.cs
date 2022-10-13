using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ContactCountriesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ContactCountries Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.contact_countries.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ContactCountriesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("ContactCountries Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ContactCountries> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ContactCountriesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ContactCountries> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ContactCountries> contactCountriesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.contact_countries.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.contact_countries.Remove(existing);
                    }
                    contactCountriesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.contact_countries.AddRange(contactCountriesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ContactCountries> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ContactCountries> contactCountries = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.contact_countries
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    contactCountries.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return contactCountries;
        }
    }
}
