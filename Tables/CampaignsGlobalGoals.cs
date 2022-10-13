#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CampaignsGlobalGoals
    {
        [Key]
        public int id { get; set; }
        public int? campaign_id { get; set; }
        public DateTime? deadline { get; set; }
        public int? goal { get; set; }
        public int? done { get; set; }
        public DateTime? last_change { get; set; }
        public DateTime? reaching_date { get; set; }
        public int? adjustment { get; set; }
        public int? lead_progress_num { get; set; }
        public string? lead_progress_interval { get; set; }
        public int? custom_amount_accepted_leads { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
