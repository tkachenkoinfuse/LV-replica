#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Lists
    {
        [Key]
        public int id { get; set; }
        public ulong? uuid { get; set; }
        public int? list_status { get; set; }
        public string? cleaning_status { get; set; }
        public int? priority { get; set; }
        public int? qc_priority { get; set; }
        public int? nt_priority { get; set; }
        public int? is_PL { get; set; }
        public int? is_GDPR { get; set; }
        public int? is_DPO { get; set; }
        public int? is_NE { get; set; }
        public string? manual_name { get; set; }
        public int? campaign_id { get; set; }
        public int? campaign_manager_id { get; set; }
        public int? team_id { get; set; }
        public int? id_user_responsible_for_report { get; set; }
        public string? verify_by { get; set; }
        public DateTime? deadline { get; set; }
        public DateTime? due_date { get; set; }
        public DateTime? generation_date { get; set; }
        public string? comments { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? max_pv_company { get; set; }
        public int? max_ov_company { get; set; }
        public int? max_drs_company { get; set; }
        public string? backup_scope { get; set; }
        public int? current_template_version_id { get; set; }
        public int? exported { get; set; }
        public int? exported_template { get; set; }
        public int? local_type { get; set; }
        public int? created_by { get; set; }
        public string? name { get; set; }
        public int? auto_accepted_pv { get; set; }
        public int? is_kostyl { get; set; }
        public int? automation_status { get; set; }
        public int? collection_id { get; set; }
        public string? collection_tag { get; set; }
        public int? parent_id { get; set; }
        public int? is_can_be_approved_version { get; set; }
        public int? is_multitouch { get; set; }
        public int? is_copy { get; set; }

    }
}
