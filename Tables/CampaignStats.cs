#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CampaignStats
    {
        [Key]
        public int campaign_id { get; set; }
        public int? local_done { get; set; }
        public int? global_done { get; set; }
    }
}
