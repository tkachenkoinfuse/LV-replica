using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignRequirementOptionsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("CampaignRequirementOptions Start!");

            List<CampaignRequirementOptions> sourceData = this.getData(intervalDays);

            Log.Logger.Information($"CampaignRequirementOptions count is: {sourceData.Count}.");

            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<CampaignRequirementOptions> campaignRequirementOptionsList = new();

                int a = 1;
                int c = 1;
                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.campaign_requirement_options.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.campaign_requirement_options.Remove(existing);
                    }

                    //if(table.is_latest==1)
                    //{
                    //    var existingLast = _baseContextTarget.campaign_requirement_options.Find(table.campaign_id, table.is_latest);
                    //    if (existingLast != null)
                    //    {
                    //        existingLast.is_latest = null;
                    //        _baseContextTarget.SaveChanges();
                    //    }

                    //}

                    //Log.Logger.Information($"{a}. Id is: {table.id}.");
                    campaignRequirementOptionsList.Add(table);
                    if (c >= 10000)
                    {
                        _baseContextTarget.SaveChanges();
                        _baseContextTarget.campaign_requirement_options.AddRange(campaignRequirementOptionsList);
                        _baseContextTarget.SaveChanges();
                        campaignRequirementOptionsList.Clear();
                        c = 1;
                    }
                    else
                    {
                        c++;
                    }
                    a++;
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.campaign_requirement_options.AddRange(campaignRequirementOptionsList);
                _baseContextTarget.SaveChanges();
            }

            Log.Logger.Information("CampaignRequirementOptions Done!");
        }
        private List<CampaignRequirementOptions> getData(int intervalDays)
        {
            //Log.Logger.Information("start reading");
            List<CampaignRequirementOptions> campaignRequirementOptions = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                DateTime dateEnd = DateTime.Now;
                DateTime dateStart = dateEnd.AddDays(-1*intervalDays);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                var tableData = _baseContextReplica.campaign_requirement_options.Where(t => t.updated_at >= dateStart);           
                foreach (var data in tableData)
                {
                    campaignRequirementOptions.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return campaignRequirementOptions;
        }
    }
}
