using Hangfire;
using ServiceWithHangfire.DAL;
using System;
using System.ComponentModel;
using System.IO;

namespace ServiceWithHangfire
{
    public class Perform
    {
        [DisplayName("JobID: {0}")]
        public void StartPerform(string jobName, AppSettings _appSettings)
        {
            RecurringJob.AddOrUpdate<TeamsImport>("Teams", (TeamsImport) => TeamsImport.RequestData("Import Teams", _appSettings.intervalDays.teams), _appSettings.cron.teams);
            RecurringJob.AddOrUpdate<SmallTeamsImport>("SmallTeams", (SmallTeamsImport) => SmallTeamsImport.RequestData("Import SmallTeams", _appSettings.intervalDays.smallTeams), _appSettings.cron.smallTeams);
            RecurringJob.AddOrUpdate<CommentsImport>("Comments", (CommentsImport) => CommentsImport.RequestData("Import Comments", _appSettings.intervalDays.comments), _appSettings.cron.comments);
            RecurringJob.AddOrUpdate<UsersImport>("Users", (UsersImport) => UsersImport.RequestData("Import Users", _appSettings.intervalDays.users), _appSettings.cron.users);
            RecurringJob.AddOrUpdate<RolesImport>("Roles", (RolesImport) => RolesImport.RequestData("Import Roles", _appSettings.intervalDays.roles), _appSettings.cron.roles);
            RecurringJob.AddOrUpdate<UserWorkShiftsImport>("UserWorkShifts", (UserWorkShiftsImport) => UserWorkShiftsImport.RequestData("Import UserWorkShifts", 0), _appSettings.cron.userWorkShifts);
            RecurringJob.AddOrUpdate<AttributesImport>("Attributes", (AttributesImport) => AttributesImport.RequestData("Import Attributes", _appSettings.intervalDays.attributes), _appSettings.cron.attributes);
            RecurringJob.AddOrUpdate<UserSchedulesImport>("UserSchedules", (UserSchedulesImport) => UserSchedulesImport.RequestData("Import UserSchedules", _appSettings.intervalDays.userSchedules), _appSettings.cron.userSchedules);
            RecurringJob.AddOrUpdate<ListsImport>("Lists", (ListsImport) => ListsImport.RequestData("Import Lists", _appSettings.intervalDays.lists), _appSettings.cron.lists);
            RecurringJob.AddOrUpdate<CampaignsImport>("Campaigns", (CampaignsImport) => CampaignsImport.RequestData("Import Campaigns", _appSettings.intervalDays.campaigns), _appSettings.cron.campaigns);
            RecurringJob.AddOrUpdate<VerificationsImport>("Verifications", (VerificationsImport) => VerificationsImport.RequestData("Import Verifications", _appSettings.intervalDays.verifications), _appSettings.cron.verifications);
            RecurringJob.AddOrUpdate<ContactsImport>("Contacts", (ContactsImport) => ContactsImport.RequestData("Import Contacts", _appSettings.intervalDays.contacts), _appSettings.cron.contacts);
            RecurringJob.AddOrUpdate<QCChecksImport>("QCChecks", (QCChecksImport) => QCChecksImport.RequestData("Import QCChecks", _appSettings.intervalDays.qcChecks), _appSettings.cron.qcChecks);
            RecurringJob.AddOrUpdate<QCCheckPartsImport>("QCCheckParts", (QCCheckPartsImport) => QCCheckPartsImport.RequestData("Import QCCheckParts", 0), _appSettings.cron.qcCheckParts);
            RecurringJob.AddOrUpdate<QCCheckReactionsImport>("QCCheckReactions", (QCCheckReactionsImport) => QCCheckReactionsImport.RequestData("Import QCCheckReactions", _appSettings.intervalDays.qcCheckReactions), _appSettings.cron.qcCheckReactions);
            RecurringJob.AddOrUpdate<TimeIntervalsAmountsImport>("TimeIntervalsAmounts", (TimeIntervalsAmountsImport) => TimeIntervalsAmountsImport.RequestData("Import TimeIntervalsAmounts", _appSettings.intervalDays.timeIntervalsAmounts), _appSettings.cron.timeIntervalsAmounts);
            RecurringJob.AddOrUpdate<ContactCampaignCoordinatorStatusesImport>("ContactCampaignCoordinatorStatuses", (ContactCampaignCoordinatorStatusesImport) => ContactCampaignCoordinatorStatusesImport.RequestData("Import ContactCampaignCoordinatorStatuses", _appSettings.intervalDays.contactCampaignCoordinatorStatuses), _appSettings.cron.contactCampaignCoordinatorStatuses);
            RecurringJob.AddOrUpdate<ContactCountriesImport>("ContactCountries", (ContactCountriesImport) => ContactCountriesImport.RequestData("Import ContactCountries", 0), _appSettings.cron.contactCountries);
            RecurringJob.AddOrUpdate<CampaignsGlobalGoalsImport>("CampaignsGlobalGoals", (CampaignsGlobalGoalsImport) => CampaignsGlobalGoalsImport.RequestData("Import CampaignsGlobalGoals", _appSettings.intervalDays.campaignsGlobalGoals), _appSettings.cron.campaignsGlobalGoals);
            RecurringJob.AddOrUpdate<CampaignsLocalGoalsImport>("CampaignsLocalGoals", (CampaignsLocalGoalsImport) => CampaignsLocalGoalsImport.RequestData("Import CampaignsLocalGoals", _appSettings.intervalDays.campaignsLocalGoals), _appSettings.cron.campaignsLocalGoals);
            RecurringJob.AddOrUpdate<CampaignCoordinatorStatusesImport>("CampaignCoordinatorStatuses", (CampaignCoordinatorStatusesImport) => CampaignCoordinatorStatusesImport.RequestData("Import CampaignCoordinatorStatuses", 0), _appSettings.cron.campaignCoordinatorStatuses);
            RecurringJob.AddOrUpdate<CampaignRequirementOptionsImport>("CampaignRequirementOptions", (CampaignRequirementOptionsImport) => CampaignRequirementOptionsImport.RequestData("Import CampaignRequirementOptions", _appSettings.intervalDays.campaignRequirementOptions), _appSettings.cron.campaignRequirementOptions);
            RecurringJob.AddOrUpdate<CampaignStatsImport>("CampaignStats", (CampaignStatsImport) => CampaignStatsImport.RequestData("Import CampaignStats",0), _appSettings.cron.campaignStats);
            RecurringJob.AddOrUpdate<CampaignStatsTriggerImport>("CampaignStatsTrigger", (CampaignStatsTriggerImport) => CampaignStatsTriggerImport.RequestData("Import CampaignStatsTrigger", 0), _appSettings.cron.campaignStatsTrigger);
            RecurringJob.AddOrUpdate<CampaignTemplateVersionsImport>("CampaignTemplateVersions", (CampaignTemplateVersionsImport) => CampaignTemplateVersionsImport.RequestData("Import CampaignTemplateVersions", _appSettings.intervalDays.campaignTemplateVersions), _appSettings.cron.campaignTemplateVersions);
            RecurringJob.AddOrUpdate<ContactTitlesImport>("ContactTitles", (ContactTitlesImport) => ContactTitlesImport.RequestData("Import ContactTitles", 0), _appSettings.cron.contactTitles);
            RecurringJob.AddOrUpdate<CountriesImport>("Countries", (CountriesImport) => CountriesImport.RequestData("Import Countries", 0), _appSettings.cron.countries);
            RecurringJob.AddOrUpdate<CountriesAliasesImport>("CountriesAliases", (CountriesAliasesImport) => CountriesAliasesImport.RequestData("Import CountriesAliases", 0), _appSettings.cron.countriesAliases);
            RecurringJob.AddOrUpdate<CountryPhoneRegionsImport>("CountryPhoneRegions", (CountryPhoneRegionsImport) => CountryPhoneRegionsImport.RequestData("Import CountryPhoneRegions", 0), _appSettings.cron.countryPhoneRegions);
            RecurringJob.AddOrUpdate<IntervalsInScheduleImport>("IntervalsInSchedule", (IntervalsInScheduleImport) => IntervalsInScheduleImport.RequestData("Import IntervalsInSchedule", _appSettings.intervalDays.intervalsInSchedule), _appSettings.cron.intervalsInSchedule);
            RecurringJob.AddOrUpdate<ListStatsImport>("ListStats", (ListStatsImport) => ListStatsImport.RequestData("Import ListStats", 0), _appSettings.cron.listStats);
            RecurringJob.AddOrUpdate<ListStatsNewCriteriasImport>("ListStatsNewCriterias", (ListStatsNewCriteriasImport) => ListStatsNewCriteriasImport.RequestData("Import ListStatsNewCriterias", 0), _appSettings.cron.listStatsNewCriterias);
            RecurringJob.AddOrUpdate<LSBatchesImport>("LSBatches", (LSBatchesImport) => LSBatchesImport.RequestData("Import LSBatches", _appSettings.intervalDays.lsBatches), _appSettings.cron.lsBatches);
            RecurringJob.AddOrUpdate<LSBatchStatusesImport>("LSBatchStatuses", (LSBatchStatusesImport) => LSBatchStatusesImport.RequestData("Import LSBatchStatuses", 0), _appSettings.cron.lsBatchStatuses);
            RecurringJob.AddOrUpdate<FeedLogImport>("FeedLog", (FeedLogImport) => FeedLogImport.RequestData("Import FeedLog", _appSettings.intervalDays.feedLog), _appSettings.cron.feedLog);
            RecurringJob.AddOrUpdate<LSLeadsImport>("LSLeads", (LSLeadsImport) => LSLeadsImport.RequestData("Import LSLeads", _appSettings.intervalDays.lsLeads), _appSettings.cron.lsLeads);
            RecurringJob.AddOrUpdate<LSLeadStatusesImport>("LSLeadStatuses", (LSLeadStatusesImport) => LSLeadStatusesImport.RequestData("Import LSLeadStatuses", 0), _appSettings.cron.lsLeadStatuses);
            RecurringJob.AddOrUpdate<ManualTimeImport>("ManualTime", (ManualTimeImport) => ManualTimeImport.RequestData("Import ManualTime", 0), _appSettings.cron.manualTime);
            RecurringJob.AddOrUpdate<ManualVerificationsImport>("ManualVerifications", (ManualVerificationsImport) => ManualVerificationsImport.RequestData("Import ManualVerifications", 0), _appSettings.cron.manualVerifications);
            //RecurringJob.AddOrUpdate<MetaFieldsImport>("MetaFields", (MetaFieldsImport) => MetaFieldsImport.RequestData("Import MetaFields", 0), _appSettings.cron.metaFields);
            RecurringJob.AddOrUpdate<OvStatusesImport>("OvStatuses", (OvStatusesImport) => OvStatusesImport.RequestData("Import OvStatuses", 0), _appSettings.cron.ovStatuses);
            RecurringJob.AddOrUpdate<PhoneReasonsImport>("PhoneReasons", (PhoneReasonsImport) => PhoneReasonsImport.RequestData("Import PhoneReasons", _appSettings.intervalDays.phoneReasons), _appSettings.cron.phoneReasons);
            RecurringJob.AddOrUpdate<ProductivityTypesImport>("ProductivityTypes", (ProductivityTypesImport) => ProductivityTypesImport.RequestData("Import ProductivityTypes", 0), _appSettings.cron.productivityTypes);
            RecurringJob.AddOrUpdate<QCStrikeCommentsImport>("QCStrikeComments", (QCStrikeCommentsImport) => QCStrikeCommentsImport.RequestData("Import QCStrikeComments", 0), _appSettings.cron.qcStrikeComments);
            RecurringJob.AddOrUpdate<QCStrikeReasonsImport>("QCStrikeReasons", (QCStrikeReasonsImport) => QCStrikeReasonsImport.RequestData("Import QCStrikeReasons", 0), _appSettings.cron.qcStrikeReasons);
            RecurringJob.AddOrUpdate<QCStrikeReasonCommentImport>("QCStrikeReasonComment", (QCStrikeReasonCommentImport) => QCStrikeReasonCommentImport.RequestData("Import QCStrikeReasonComment", 0), _appSettings.cron.qcStrikeReasonComment);
            RecurringJob.AddOrUpdate<QCStrikeVerdictsImport>("QCStrikeVerdicts", (QCStrikeVerdictsImport) => QCStrikeVerdictsImport.RequestData("Import QCStrikeVerdicts", 0), _appSettings.cron.qcStrikeVerdicts);
            RecurringJob.AddOrUpdate<RejectsImport>("Rejects", (RejectsImport) => RejectsImport.RequestData("Import Rejects", 0), _appSettings.cron.rejects);
            RecurringJob.AddOrUpdate<TeamPairsImport>("TeamPairs", (TeamPairsImport) => TeamPairsImport.RequestData("Import TeamPairs", 0), _appSettings.cron.teamPairs);
            RecurringJob.AddOrUpdate<TimeIntervalsHourImport>("TimeIntervalsHour", (TimeIntervalsHourImport) => TimeIntervalsHourImport.RequestData("Import TimeIntervalsHour", _appSettings.intervalDays.timeIntervalsHour), _appSettings.cron.timeIntervalsHour);
            RecurringJob.AddOrUpdate<TypesOfCampaignsImport>("TypesOfCampaigns", (TypesOfCampaignsImport) => TypesOfCampaignsImport.RequestData("Import TypesOfCampaigns", _appSettings.intervalDays.typesOfCampaigns), _appSettings.cron.typesOfCampaigns);
            RecurringJob.AddOrUpdate<UniqueRejectsImport>("UniqueRejects", (UniqueRejectsImport) => UniqueRejectsImport.RequestData("Import UniqueRejects", 0), _appSettings.cron.uniqueRejects);
            RecurringJob.AddOrUpdate<UserAttributesImport>("UserAttributes", (UserAttributesImport) => UserAttributesImport.RequestData("Import UserAttributes", _appSettings.intervalDays.userAttributes), _appSettings.cron.userAttributes);
            RecurringJob.AddOrUpdate<UserExpectedWorkingTimeImport>("UserExpectedWorkingTime", (UserExpectedWorkingTimeImport) => UserExpectedWorkingTimeImport.RequestData("Import UserExpectedWorkingTime", _appSettings.intervalDays.userExpectedWorkingTime), _appSettings.cron.userExpectedWorkingTime);
            RecurringJob.AddOrUpdate<UserRecordsImport>("UserRecords", (UserRecordsImport) => UserRecordsImport.RequestData("Import UserRecords", _appSettings.intervalDays.userRecords), _appSettings.cron.userRecords);
        }
    }


    public static class WriteToFile
    {
        public static void writeLog(string text, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(text);
            }
        }
    }
}
