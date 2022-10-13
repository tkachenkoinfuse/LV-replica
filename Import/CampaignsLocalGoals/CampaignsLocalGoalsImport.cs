using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignsLocalGoalsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("CampaignsLocalGoals Start!");

            List<CampaignsLocalGoals> sourceData = this.getData(intervalDays);

            Log.Logger.Information($"CampaignsLocalGoals count is: {sourceData.Count}.");

            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<CampaignsLocalGoals> campaignsLocalGoalsList = new();

                int a = 1;
                int c = 1;
                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.campaigns_local_goals.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.campaigns_local_goals.Remove(existing);
                    }
                    //Log.Logger.Information($"{a}. Id is: {table.id}.");
                    campaignsLocalGoalsList.Add(table);
                    if (c >= 10000)
                    {
                        _baseContextTarget.SaveChanges();
                        _baseContextTarget.campaigns_local_goals.AddRange(campaignsLocalGoalsList);
                        _baseContextTarget.SaveChanges();
                        campaignsLocalGoalsList.Clear();
                        c = 1;
                    }
                    else
                    {
                        c++;
                    }
                    a++;
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.campaigns_local_goals.AddRange(campaignsLocalGoalsList);
                _baseContextTarget.SaveChanges();
            }

            Log.Logger.Information("CampaignsLocalGoals Done!");
        }
        private List<CampaignsLocalGoals> getData(int intervalDays)
        {
            //Log.Logger.Information("start reading");
            List<CampaignsLocalGoals> campaignsLocalGoals = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                DateTime dateEnd = DateTime.Now;
                DateTime dateStart = dateEnd.AddDays(-1*intervalDays);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                var tableData = _baseContextReplica.campaigns_local_goals.Where(t => t.updated_at >= dateStart);           
                foreach (var data in tableData)
                {
                    campaignsLocalGoals.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return campaignsLocalGoals;
        }
    }
}
