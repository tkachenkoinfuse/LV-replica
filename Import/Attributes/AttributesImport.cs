using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class AttributesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
                List<Attributes> sourceData = this.getData(intervalDays);

                using (var _baseContextTarget = new BaseContextTarget())
                {
                    List<Attributes> attributesList = new();

                    int c = 1;
                    foreach (var table in sourceData)
                    {
                        var existing = _baseContextTarget.attributes.Find(table.id);
                        if (existing != null)
                        {
                            _baseContextTarget.attributes.Remove(existing);
                        }
                        //Log.Logger.Information($"id is: {table.id}");
                        attributesList.Add(table);
                        if (c >= 1000)
                        {
                            _baseContextTarget.SaveChanges();
                            _baseContextTarget.attributes.AddRange(attributesList);
                            _baseContextTarget.SaveChanges();
                            attributesList.Clear();
                            c = 1;
                        }
                        else
                        {
                            c++;
                        }

                    }
                    _baseContextTarget.SaveChanges();
                    _baseContextTarget.attributes.AddRange(attributesList);
                    _baseContextTarget.SaveChanges();
            }
            Log.Logger.Information("Attributes Done!");
        }
        private List<Attributes> getData(int intervalDays)
        {
            //Log.Logger.Information("start reading");
            List<Attributes> attributes = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                DateTime dateEnd = DateTime.Now;
                DateTime dateStart = dateEnd.AddDays(-1 * intervalDays);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                var tableData = _baseContextReplica.attributes.Where(t => t.updated_at >= dateStart);
                foreach (var data in tableData)
                {
                    attributes.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return attributes;
        }
    }
}
