using Microsoft.EntityFrameworkCore;

namespace ServiceWithHangfire
{
    public class BaseContextTarget : DbContext
    {
        // t1 - название таблицы в БД
        public DbSet<Teams> teams { get; set; }
        public DbSet<SmallTeams> small_teams { get; set; }
        public DbSet<Comments> comments { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<UserWorkShifts> user_work_shifts { get; set; }
        public DbSet<Attributes> attributes { get; set; }
        public DbSet<UserSchedules> user_schedules { get; set; }
        public DbSet<Lists> lists { get; set; }
        public DbSet<Verifications> verifications { get; set; }
        public DbSet<Campaigns> campaigns { get; set; }
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<QCChecks> qc_checks { get; set; }
        public DbSet<QCCheckParts> qc_check_parts { get; set; }
        public DbSet<QCCheckReactions> qc_check_reactions { get; set; }
        public DbSet<TimeIntervalsAmounts> time_intervals_amounts { get; set; }
        public DbSet<ContactCampaignCoordinatorStatuses> contact_campaign_coordinator_statuses { get; set; }
        public DbSet<ContactCountries> contact_countries { get; set; }
        public DbSet<CampaignsGlobalGoals> campaigns_global_goals { get; set; }
        public DbSet<CampaignsLocalGoals> campaigns_local_goals { get; set; }
        public DbSet<CampaignCoordinatorStatuses> campaign_coordinator_statuses { get; set; }
        public DbSet<CampaignRequirementOptions> campaign_requirement_options { get; set; }
        public DbSet<CampaignStats> campaign_stats { get; set; }
        public DbSet<CampaignStatsTrigger> campaign_stats_trigger { get; set; }
        public DbSet<CampaignTemplateVersions> campaign_template_versions { get; set; }
        public DbSet<ContactTitles> contact_titles { get; set; }
        public DbSet<Countries> countries { get; set; }
        public DbSet<CountriesAliases> countries_aliases { get; set; }
        public DbSet<CountryPhoneRegions> country_phone_regions { get; set; }
        public DbSet<IntervalsInSchedule> intervals_in_schedule { get; set; }
        public DbSet<ListStats> list_stats { get; set; }
        public DbSet<ListStatsNewCriterias> list_stats_new_criterias { get; set; }
        public DbSet<LSBatches> l_s_batches { get; set; }
        public DbSet<LSBatchStatuses> l_s_batch_statuses { get; set; }
        public DbSet<FeedLog> feed_log { get; set; }
        public DbSet<LSLeads> l_s_leads { get; set; }
        public DbSet<LSLeadStatuses> l_s_lead_statuses { get; set; }
        public DbSet<ManualTime> manual_time { get; set; }
        public DbSet<ManualVerifications> manual_verifications { get; set; }
        public DbSet<MetaFields> meta_fields { get; set; }
        public DbSet<OvStatuses> ov_statuses { get; set; }
        public DbSet<PhoneReasons> phone_reasons { get; set; }
        public DbSet<ProductivityTypes> productivity_types { get; set; }
        public DbSet<QCStrikeComments> qc_strike_comments { get; set; }
        public DbSet<QCStrikeReasons> qc_strike_reasons { get; set; }
        public DbSet<QCStrikeReasonComment> qc_strike_reason_comment { get; set; }
        public DbSet<QCStrikeVerdicts> qc_strike_verdicts { get; set; }
        public DbSet<Rejects> rejects { get; set; }
        public DbSet<TeamPairs> team_pairs { get; set; }
        public DbSet<TimeIntervalsHour> time_intervals_hour { get; set; }
        public DbSet<TypesOfCampaigns> types_of_campaigns { get; set; }
        public DbSet<UniqueRejects> unique_rejects { get; set; }
        public DbSet<UserAttributes> user_attributes { get; set; }
        public DbSet<UserExpectedWorkingTime> user_expected_working_time { get; set; }
        public DbSet<UserRecords> user_records { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teams>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<SmallTeams>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Comments>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Users>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Roles>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<UserWorkShifts>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Attributes>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Lists>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Campaigns>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Verifications>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Contacts>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCChecks>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCCheckParts>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCCheckReactions>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<TimeIntervalsAmounts>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<ContactCountries>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CampaignsGlobalGoals>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CampaignsLocalGoals>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CampaignCoordinatorStatuses>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CampaignRequirementOptions>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CampaignTemplateVersions>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<ContactTitles>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Countries>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CountriesAliases>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<CountryPhoneRegions>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<LSBatches>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<LSBatchStatuses>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<FeedLog>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<LSLeads>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<LSLeadStatuses>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<MetaFields>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<OvStatuses>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<PhoneReasons>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<ProductivityTypes>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCStrikeComments>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCStrikeReasons>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCStrikeReasonComment>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<QCStrikeVerdicts>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<Rejects>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<TeamPairs>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<UniqueRejects>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<UserAttributes>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<UserExpectedWorkingTime>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<UserRecords>().HasKey(tbl => new { tbl.id });
            modelBuilder.Entity<ListStats>().HasKey(tbl => new { tbl.list_id });
            modelBuilder.Entity<ListStatsNewCriterias>().HasKey(tbl => new { tbl.list_id });
            modelBuilder.Entity<ContactCampaignCoordinatorStatuses>().HasKey(tbl => new { tbl.contact_id });
            modelBuilder.Entity<CampaignStats>().HasKey(tbl => new { tbl.campaign_id });
            modelBuilder.Entity<CampaignStatsTrigger>().HasKey(tbl => new { tbl.campaign_id });
            modelBuilder.Entity<ManualTime>().HasKey(tbl => new { tbl.record_id });
            modelBuilder.Entity<ManualVerifications>().HasKey(tbl => new { tbl.record_id });
            modelBuilder.Entity<UserSchedules>().HasKey(tbl => new { tbl.user_id, tbl.day });
            modelBuilder.Entity<TypesOfCampaigns>().HasKey(tbl => new { tbl.id, tbl.short_name, tbl.long_name });
            modelBuilder.Entity<TimeIntervalsHour>().HasKey(tbl => new { tbl.user_id, tbl.day, tbl.hour });
            modelBuilder.Entity<IntervalsInSchedule>().HasKey(tbl => new { tbl.user_id, tbl.day, tbl.hour, tbl.type_id });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Constant.connectionStringTargetDB);
        }
    }
}
