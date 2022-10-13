#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Campaigns
    {
        [Key]
        public int id { get; set; }
        public int? client_id { get; set; }
        public string? client_name { get; set; }
        public int? campaign_id { get; set; }
        public string? campaign_name { get; set; }
        public int? contacts_per_company { get; set; }
        public int? buffer_contacts_per_company { get; set; }
        public int? team_id { get; set; }
        public int? status_id { get; set; }
        public int? critical { get; set; }
        public string? job_titles { get; set; }
        public string? industries { get; set; }
        public string? description { get; set; }
        public int? employees_min { get; set; }
        public int? employees_max { get; set; }
        public int? revenue_min { get; set; }
        public int? revenue_max { get; set; }
        public string? links { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public string? assets { get; set; }
        public int? priority { get; set; }
        public DateTime? start_date { get; set; }
        public int? type { get; set; }
        public string? client_type { get; set; }
        public int? team_pair_id { get; set; }
        public string? cid { get; set; }
        public int? current_template_version_id { get; set; }
        public int? recalculation_flag { get; set; }
        public int? three_month_rule_pv { get; set; }
        public int? last_lists_limits_id { get; set; }
        public int? max_ov_company { get; set; }
        public int? max_drs_company { get; set; }
        public int? max_pv_company { get; set; }
       
    }
}
