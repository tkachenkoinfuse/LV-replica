#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CampaignStatsTrigger
    {
        [Key]
        public int campaign_id { get; set; }
        public int? local_done { get; set; }
        public int? global_done { get; set; }
    }
}
