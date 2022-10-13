using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignsGlobalGoalsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("CampaignsGlobalGoals Start!");

            List<CampaignsGlobalGoals> sourceData = this.getData(intervalDays);

            Log.Logger.Information($"CampaignsGlobalGoals count is: {sourceData.Count}.");

            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<CampaignsGlobalGoals> campaignsGlobalGoalsList = new();

                int a = 1;
                int c = 1;
                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.campaigns_global_goals.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.campaigns_global_goals.Remove(existing);
                    }
                    //Log.Logger.Information($"{a}. Id is: {table.id}.");
                    campaignsGlobalGoalsList.Add(table);
                    if (c >= 10000)
                    {
                        _baseContextTarget.SaveChanges();
                        _baseContextTarget.campaigns_global_goals.AddRange(campaignsGlobalGoalsList);
                        _baseContextTarget.SaveChanges();
                        campaignsGlobalGoalsList.Clear();
                        c = 1;
                    }
                    else
                    {
                        c++;
                    }
                    a++;
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.campaigns_global_goals.AddRange(campaignsGlobalGoalsList);
                _baseContextTarget.SaveChanges();
            }

            Log.Logger.Information("CampaignsGlobalGoals Done!");
        }
        private List<CampaignsGlobalGoals> getData(int intervalDays)
        {
            //Log.Logger.Information("start reading");
            List<CampaignsGlobalGoals> campaignsGlobalGoals = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                DateTime dateEnd = DateTime.Now;
                DateTime dateStart = dateEnd.AddDays(-1*intervalDays);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                var tableData = _baseContextReplica.campaigns_global_goals.Where(t => t.updated_at >= dateStart);           
                foreach (var data in tableData)
                {
                    campaignsGlobalGoals.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return campaignsGlobalGoals;
        }
    }
}
