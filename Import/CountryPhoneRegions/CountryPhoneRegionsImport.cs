using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CountryPhoneRegionsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("CountryPhoneRegions Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.country_phone_regions.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<CountryPhoneRegionsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("CountryPhoneRegions Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<CountryPhoneRegions> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<CountryPhoneRegionsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<CountryPhoneRegions> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<CountryPhoneRegions> countryPhoneRegionsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.country_phone_regions.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.country_phone_regions.Remove(existing);
                    }
                    countryPhoneRegionsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.country_phone_regions.AddRange(countryPhoneRegionsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<CountryPhoneRegions> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<CountryPhoneRegions> countryPhoneRegions = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.country_phone_regions
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    countryPhoneRegions.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return countryPhoneRegions;
        }
    }
}
