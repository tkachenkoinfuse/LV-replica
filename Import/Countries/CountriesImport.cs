using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CountriesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("Countries Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.countries.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<CountriesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("Countries Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<Countries> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<CountriesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<Countries> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<Countries> countriesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.countries.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.countries.Remove(existing);
                    }
                    countriesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.countries.AddRange(countriesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<Countries> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<Countries> countries = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.countries
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    countries.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return countries;
        }
    }
}
