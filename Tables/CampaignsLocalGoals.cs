#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CampaignsLocalGoals
    {
        [Key]
        public int id { get; set; }
        public int? campaign_id { get; set; }
        public int? history_parent_id { get; set; }
        public DateTime? due_date { get; set; }
        public int? goal { get; set; }
        public int? goal_ov { get; set; }
        public int? done { get; set; }
        public DateTime? last_change { get; set; }
        public DateTime? reaching_date { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public float? coef_ov { get; set; }
        public short? auto_daily_reset { get; set; }
    }
}
