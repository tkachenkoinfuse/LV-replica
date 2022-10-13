using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("Campaigns Start!");

            List<Campaigns> sourceData = this.getData(intervalDays);

            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<Campaigns> campaignsList = new();

                int a = 1;
                int c = 1;
                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.campaigns.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.campaigns.Remove(existing);
                    }
                    //Log.Logger.Information($"{a}. Id is: {table.id}.");
                    campaignsList.Add(table);
                    if (c >= 10000)
                    {
                        _baseContextTarget.SaveChanges();
                        _baseContextTarget.campaigns.AddRange(campaignsList);
                        _baseContextTarget.SaveChanges();
                        campaignsList.Clear();
                        c = 1;
                    }
                    else
                    {
                        c++;
                    }
                    a++;
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.campaigns.AddRange(campaignsList);
                _baseContextTarget.SaveChanges();
            }

            Log.Logger.Information("Campaigns Done!");
        }
        private List<Campaigns> getData(int intervalDays)
        {
            //Log.Logger.Information("start reading");
            List<Campaigns> campaigns = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                DateTime dateEnd = DateTime.Now;
                DateTime dateStart = dateEnd.AddDays(-1*intervalDays);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                var tableData = _baseContextReplica.campaigns.Where(t => t.updated_at >= dateStart);           
                foreach (var data in tableData)
                {
                    campaigns.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return campaigns;
        }
    }
}
