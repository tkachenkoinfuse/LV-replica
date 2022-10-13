#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Contacts
    {
        [Key]
        public int id { get; set; }
        public int? status { get; set; }
        public string? approve_status { get; set; }
        public UInt64? contact_id { get; set; }
        public int? email_id { get; set; }
        public int? campaign_id { get; set; }
        public int? list_id { get; set; }
        public int? ext_list_id { get; set; }
        public int? block_id { get; set; }
        public int? company_id { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public int? phone_reason_id { get; set; }
        public int? self_phone_reason_id { get; set; }
        public int? pv_status_id { get; set; }
        public int? ov_status_id { get; set; }
        public int? self_ov_status_id { get; set; }
        public int? ov_status_id_for_backups { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? locked { get; set; } 
        public DateTime? locked_at { get; set; }
        public int? locked_by { get; set; }
        public int? job_level { get; set; }
        public DateTime? pv_date { get; set; }
        public int? reported { get; set; } 
        public int? reported_template { get; set; } 
        public DateTime? phone_verified { get; set; } 
        public DateTime? locked_since { get; set; }
        public int? domain_id { get; set; }
        public int? lvo { get; set; }
        public int? lvp { get; set; }
        public int? dro { get; set; }
        public int? comment_id { get; set; }
        public int? final_approve { get; set; } 
        public int? disable_backup { get; set; } 
        public int? contact_qc_id { get; set; }
        public int? lvc { get; set; }
        public int? acquired_list_id { get; set; }
        public int? moved_to_list_id { get; set; } 
        public int? email_elt { get; set; } 
        public int? previous_pv_comment_id { get; set; } 
        public int? reset_for_goal { get; set; }  
        public int? cm_status { get; set; } 
        public int? is_recheck_company { get; set; } 
        public DateTime? previous_ov_date { get; set; } 
        public DateTime? address_date { get; set; } 
        public UInt64? update_uu { get; set; } 
        public int? non_retrievable { get; set; } 
        public int? source_id { get; set; } 

    }
}
