using Serilog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignStatsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var campaignStats = _baseContextReplica.campaign_stats.Where(t => t.campaign_id > 0);

                using (var _baseContextTarget = new BaseContextTarget())
                {
                    List<CampaignStats> campaignStatsList = new();

                    int c = 1;
                    foreach (var table in campaignStats)
                    {
                        var existing = _baseContextTarget.campaign_stats.Find(table.campaign_id);
                        if (existing != null)
                        {
                            _baseContextTarget.campaign_stats.Remove(existing);
                        }
                        //Log.Logger.Information($"id is: {table.id}");
                        campaignStatsList.Add(table);
                        if (c >= 1000)
                        {
                            _baseContextTarget.SaveChanges();
                            _baseContextTarget.campaign_stats.AddRange(campaignStatsList);
                            _baseContextTarget.SaveChanges();
                            campaignStatsList.Clear();
                            c = 1;
                        }
                        else
                        {
                            c++;
                        }

                    }
                    _baseContextTarget.SaveChanges();
                    _baseContextTarget.campaign_stats.AddRange(campaignStatsList);
                    _baseContextTarget.SaveChanges();
                }
            }
            Log.Logger.Information("CampaignStats Done!");
        }
    }
}
