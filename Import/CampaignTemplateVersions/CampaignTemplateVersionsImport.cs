using Hangfire;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ServiceWithHangfire
{
    public class CampaignTemplateVersionsImport
    {
        [DisplayName("JobID: {0}")]
        public void RequestData(string jobName, int intervalDays)
        {
            Log.Logger.Information("CampaignTemplateVersions Start!");
            DateTime dateEnd = DateTime.Now;
            DateTime dateStart = dateEnd;
            DateTime deadline = dateEnd.AddDays(-1 * intervalDays);
            int i = 0;
            do
            {
                Console.WriteLine($"i is: {i}.");
                if (i > 0)
                    dateEnd = dateStart;

                dateStart = dateEnd.AddHours(-24);
                string dateStart_ = DateTime.Parse(dateStart.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                string dateEnd_ = DateTime.Parse(dateEnd.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"dateStart is: {dateStart_}.");
                Console.WriteLine($"dateEnd is: {dateEnd_}.");
                BackgroundJob.Enqueue<CampaignTemplateVersionsImport>(j => j.RequestPart(dateStart, dateEnd));
                i++;

            }
            while (dateStart > deadline);

            Log.Logger.Information("CampaignTemplateVersions Done!");
        }

        public void RequestPart(DateTime dateStart, DateTime dateEnd)
        {
            int start = 0;
            int cnt = 0;
            do
            {
                //Log.Logger.Information($"start is: {start}.");
                List<CampaignTemplateVersions> sourceData = this.getData(dateStart, dateEnd, start);
                BackgroundJob.Enqueue<CampaignTemplateVersionsImport>(j => j.writeData(sourceData));
                start += 10000;
                if (sourceData.Count > 0)
                    cnt = sourceData.Count;
                else
                    cnt = 0;
                Log.Logger.Information($"Count is: {cnt}.");
            }
            while (cnt > 0);
        }
        public void writeData(List<CampaignTemplateVersions> sourceData)
        {
            using (var _baseContextTarget = new BaseContextTarget())
            {
                List<CampaignTemplateVersions> campaignTemplateVersionsList = new();

                foreach (var table in sourceData)
                {
                    var existing = _baseContextTarget.campaign_template_versions.Find(table.id);
                    if (existing != null)
                    {
                        _baseContextTarget.campaign_template_versions.Remove(existing);
                    }
                    campaignTemplateVersionsList.Add(table);
                }
                _baseContextTarget.SaveChanges();
                _baseContextTarget.campaign_template_versions.AddRange(campaignTemplateVersionsList);
                _baseContextTarget.SaveChanges();
            }
        }
        private List<CampaignTemplateVersions> getData(DateTime dateStart, DateTime dateEnd, int start)
        {
            //Log.Logger.Information("start reading");
            List<CampaignTemplateVersions> campaignTemplateVersions = new();
            using (var _baseContextReplica = new BaseContextReplica())
            {
                var tableData = _baseContextReplica.campaign_template_versions
                    .Where(t => (t.updated_at >= dateStart && t.updated_at < dateEnd))
                    .Skip(start)
                    .Take(10000)
                    .OrderBy(t => t.updated_at);
                foreach (var data in tableData)
                {
                    campaignTemplateVersions.Add(data);
                }
            }
            //Log.Logger.Information("end reading");
            return campaignTemplateVersions;
        }
    }
}
