#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CampaignRequirementOptions
    {
        [Key]
        public int id { get; set; }
        public int? campaign_id { get; set; }
        public short? is_latest { get; set; }
        public int? user_id { get; set; }
        public DateTime? last_change { get; set; }
        public short? check_fields_employees { get; set; }
        public short? check_fields_revenue { get; set; }
        public short? check_fields_industry { get; set; }
        public short? check_fields_subindustry { get; set; }
        public short? check_fields_address { get; set; }
        public short? other_reject_proof_not_found { get; set; }
        public short? other_reject_personal_email { get; set; }
        public short? other_pv_needed { get; set; }
        public short? other_qc_verification_needed { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? geo { get; set; }
        public string? job_titles { get; set; }
        public string? industries { get; set; }
        public short? validate_repeating_digits { get; set; }
        public short? validate_sequent_digits { get; set; }
        public short? validate_landline_check { get; set; }
        public short? validate_flawless_pv { get; set; }
        public short? employees_empty { get; set; }
        public short? employees_skip { get; set; }
        public short? revenue_empty { get; set; }
        public short? revenue_skip { get; set; }
        public short? geo_empty { get; set; }
        public short? geo_skip { get; set; }
        public short? industry_skip { get; set; }
        public short? title_empty { get; set; }
        
    }
}
