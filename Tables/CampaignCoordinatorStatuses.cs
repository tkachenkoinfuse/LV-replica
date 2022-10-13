#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CampaignCoordinatorStatuses
    {
        [Key]
        public int id { get; set; }
        public string? short_name { get; set; }
        public string? long_name { get; set; }
        public string? class_name { get; set; }
        public short? order { get; set; }
        public short? parent_id { get; set; }
        
    }
}
