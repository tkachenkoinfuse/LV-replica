using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CountriesAliasesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("CountriesAliases Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.countries_aliases.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<CountriesAliasesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("CountriesAliases Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<CountriesAliases> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<CountriesAliasesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<CountriesAliases> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<CountriesAliases> countriesAliasesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.countries_aliases.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.countries_aliases.Remove(existing);
                    }
                    countriesAliasesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.countries_aliases.AddRange(countriesAliasesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<CountriesAliases> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<CountriesAliases> countriesAliases = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.countries_aliases
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    countriesAliases.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return countriesAliases;
        }
    }
}
