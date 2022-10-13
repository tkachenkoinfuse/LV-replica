using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class ProductivityTypesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("ProductivityTypes Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.productivity_types.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<ProductivityTypesImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;
                }
                while (idStart > 0);
            }
            Log.Logger.Information("ProductivityTypes Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<ProductivityTypes> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<ProductivityTypesImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<ProductivityTypes> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<ProductivityTypes> productivityTypesList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.productivity_types.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.productivity_types.Remove(existing);
                    }
                    productivityTypesList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.productivity_types.AddRange(productivityTypesList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<ProductivityTypes> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<ProductivityTypes> productivityTypes = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.productivity_types
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    productivityTypes.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return productivityTypes;
        }
    }
}
