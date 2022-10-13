using Serilog;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignCoordinatorStatusesImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var campaignCoordinatorStatuses = _baseContextReplica.campaign_coordinator_statuses.Where(t => t.id>0);

                using (var _baseContextTarget = new BaseContextTarget())
                {
                    List<CampaignCoordinatorStatuses> campaignCoordinatorStatusesList = new();

                    int c = 1;
                    foreach (var table in campaignCoordinatorStatuses)
                    {
                        var existing = _baseContextTarget.campaign_coordinator_statuses.Find(table.id);
                        if (existing != null)
                        {
                            _baseContextTarget.campaign_coordinator_statuses.Remove(existing);
                        }
                        //Log.Logger.Information($"id is: {table.id}");
                        campaignCoordinatorStatusesList.Add(table);
                        if (c >= 1000)
                        {
                            _baseContextTarget.SaveChanges();
                            _baseContextTarget.campaign_coordinator_statuses.AddRange(campaignCoordinatorStatusesList);
                            _baseContextTarget.SaveChanges();
                            campaignCoordinatorStatusesList.Clear();
                            c = 1;
                        }
                        else
                        {
                            c++;
                        }

                    }
                    _baseContextTarget.SaveChanges();
                    _baseContextTarget.campaign_coordinator_statuses.AddRange(campaignCoordinatorStatusesList);
                    _baseContextTarget.SaveChanges();
                }
            }
            Log.Logger.Information("CampaignCoordinatorStatuses Done!");
        }
    }
}
