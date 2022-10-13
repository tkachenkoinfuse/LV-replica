using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class MetaFieldsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("MetaFields Start!");
            int idStart = 0;
            using (var _baseContextReplica = new BaseContextReplica())
            {
                int idEnd = _baseContextReplica.meta_fields.Max(e => e.id);
                do
                {
                    idStart = idEnd - 10000;
                    if (idStart < 0)
                        idStart = 0;
                    Console.WriteLine($"id start: {idStart}. id end: {idEnd}");
                    BackgroundJob.Enqueue<MetaFieldsImport>(j => j.RequestPart(idStart, idEnd));
                    idEnd = idStart - 1;

                }
                while (idStart > 0);
            }
            Log.Logger.Information("MetaFields Done!");
        }

        public void RequestPart(int idStart, int idEnd)
        {
            int cnt = 0;
            List<MetaFields> sourceData = this.getData(idStart, idEnd);
            if (sourceData.Count > 0)
            {
                BackgroundJob.Enqueue<MetaFieldsImport>(j => j.writeData(sourceData));
                cnt = sourceData.Count;
                Log.Logger.Information($"Count is: {cnt}.");
            }

        }
        public void writeData(List<MetaFields> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<MetaFields> metaFieldsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.meta_fields.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.meta_fields.Remove(existing);
                    }
                    metaFieldsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.meta_fields.AddRange(metaFieldsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<MetaFields> getData(int idStart, int idEnd)
        {
            //Log.Logger.Information("start reading");
            List<MetaFields> metaFields = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.meta_fields
                    .Where(t => (t.id >= idStart && t.id <= idEnd))
                    .OrderBy(t => (t.id));
                foreach (var data in tableData)
                {
                    metaFields.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return metaFields;
        }
    }
}
