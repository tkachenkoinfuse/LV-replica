namespace ServiceWithHangfire.DAL
{
    public class AppSettings
    {
        public Cron cron { get; set; }
        public string applicationUrl { get; set; }
        public IntervalDays intervalDays { get; set; }
    }
    public class Cron
    {
        public string teams { get; set; }
        public string smallTeams { get; set; }
        public string comments { get; set; }
        public string users { get; set; }
        public string roles { get; set; }
        public string userWorkShifts { get; set; }
        public string attributes { get; set; }
        public string userSchedules { get; set; }
        public string lists { get; set; }
        public string campaigns { get; set; }
        public string verifications { get; set; }
        public string contacts { get; set; }
        public string qcChecks { get; set; }
        public string qcCheckParts { get; set; }
        public string qcCheckReactions { get; set; }
        public string timeIntervalsAmounts { get; set; }
        public string contactCampaignCoordinatorStatuses { get; set; }
        public string contactCountries { get; set; }
        public string campaignsGlobalGoals { get; set; }
        public string campaignsLocalGoals { get; set; }
        public string campaignCoordinatorStatuses { get; set; }
        public string campaignRequirementOptions { get; set; }
        public string campaignStats { get; set; }
        public string campaignStatsTrigger { get; set; }
        public string campaignTemplateVersions { get; set; }
        public string contactTitles { get; set; }
        public string countries { get; set; }
        public string countriesAliases { get; set; }
        public string countryPhoneRegions { get; set; }
        public string intervalsInSchedule { get; set; }
        public string listStats { get; set; }
        public string listStatsNewCriterias { get; set; }
        public string lsBatches { get; set; }
        public string lsBatchStatuses { get; set; }
        public string feedLog { get; set; }
        public string lsLeads { get; set; }
        public string lsLeadStatuses { get; set; }
        public string manualTime { get; set; }
        public string manualVerifications { get; set; }
        public string metaFields { get; set; }
        public string ovStatuses { get; set; }
        public string phoneReasons { get; set; }
        public string productivityTypes { get; set; }
        public string qcStrikeComments { get; set; }
        public string qcStrikeReasons { get; set; }
        public string qcStrikeReasonComment { get; set; }
        public string qcStrikeVerdicts { get; set; }
        public string rejects { get; set; }
        public string teamPairs { get; set; }
        public string timeIntervalsHour { get; set; }
        public string typesOfCampaigns { get; set; }
        public string uniqueRejects { get; set; }
        public string userAttributes { get; set; }
        public string userExpectedWorkingTime { get; set; }
        public string userRecords { get; set; }
    }
    public class IntervalDays
    {
        public int teams { get; set; }
        public int smallTeams { get; set; }
        public int comments { get; set; }
        public int users { get; set; }
        public int roles { get; set; }
        public int attributes { get; set; }
        public int userSchedules { get; set; }
        public int lists { get; set; }
        public int campaigns { get; set; }
        public int verifications { get; set; }
        public int contacts { get; set; }
        public int qcChecks { get; set; }
        public int qcCheckReactions { get; set; }
        public int timeIntervalsAmounts { get; set; }
        public int contactCampaignCoordinatorStatuses { get; set; }
        public int campaignsGlobalGoals { get; set; }
        public int campaignsLocalGoals { get; set; }
        public int campaignRequirementOptions { get; set; }
        public int campaignTemplateVersions { get; set; }
        public int intervalsInSchedule { get; set; }
        public int lsBatches { get; set; }
        public int feedLog { get; set; }
        public int lsLeads { get; set; }
        public int phoneReasons { get; set; }
        public int timeIntervalsHour { get; set; }
        public int typesOfCampaigns { get; set; }
        public int userAttributes { get; set; }
        public int userExpectedWorkingTime { get; set; }
        public int userRecords { get; set; }
    }
}
