#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ListStatsNewCriterias
    {
        [Key]
        public int list_id { get; set; }
        public int? campaign_id { get; set; }
        public int? accepted_absolute { get; set; }
        public int? bad_ovpv { get; set; }
        public int? checked_leads { get; set; }
        public int? backup_verified { get; set; }
        public int? q3 { get; set; }
        
    }
}
