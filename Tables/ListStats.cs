#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ListStats
    {
        [Key]
        public int list_id { get; set; }
        public int? campaign_id { get; set; }
        public short? in_archive { get; set; }
        public int? sent { get; set; }
        public int? ov_accepted { get; set; }
        public int? ov_accepted_pv { get; set; }
        public int? local_done { get; set; }
        public int? global_done { get; set; }
        public int? ov_accepted_unsuccessful_pv { get; set; }
        public int? ov_accepted_ov { get; set; }
        public int? ov_rejected { get; set; }
        public int? ov_bad { get; set; }
        public int? ov_backup { get; set; }
        public int? ov_unchecked { get; set; }
        public int? ov_verified { get; set; }
        public int? q3_other { get; set; }
        public int? q3_other_pv { get; set; }
        public int? pv_verified { get; set; }
        public int? pv_remaining { get; set; }
        public int? accepted_absolute { get; set; }
        public int? bad_ovpv { get; set; }
        public int? checked_leads { get; set; }
        public int? backup_verified { get; set; }
        public int? qc_verified_total { get; set; }
        public int? qc_verified_strikes { get; set; }
        public int? qc_verified_comments { get; set; }
        public int? qc_verified_approved { get; set; }
        public int? q3 { get; set; }
    }
}
