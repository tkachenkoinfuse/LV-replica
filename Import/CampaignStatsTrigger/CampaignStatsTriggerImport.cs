using Serilog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignStatsTriggerImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var campaignStatsTrigger = _baseContextReplica.campaign_stats_trigger.Where(t => t.campaign_id > 0);

                using (var _baseContextTarget = new BaseContextTarget())
                {
                    List<CampaignStatsTrigger> campaignStatsTriggerList = new();

                    int c = 1;
                    foreach (var table in campaignStatsTrigger)
                    {
                        var existing = _baseContextTarget.campaign_stats_trigger.Find(table.campaign_id);
                        if (existing != null)
                        {
                            _baseContextTarget.campaign_stats_trigger.Remove(existing);
                        }
                        //Log.Logger.Information($"id is: {table.id}");
                        campaignStatsTriggerList.Add(table);
                        if (c >= 1000)
                        {
                            _baseContextTarget.SaveChanges();
                            _baseContextTarget.campaign_stats_trigger.AddRange(campaignStatsTriggerList);
                            _baseContextTarget.SaveChanges();
                            campaignStatsTriggerList.Clear();
                            c = 1;
                        }
                        else
                        {
                            c++;
                        }

                    }
                    _baseContextTarget.SaveChanges();
                    _baseContextTarget.campaign_stats_trigger.AddRange(campaignStatsTriggerList);
                    _baseContextTarget.SaveChanges();
                }
            }
            Log.Logger.Information("CampaignStatsTrigger Done!");
        }
    }
}
