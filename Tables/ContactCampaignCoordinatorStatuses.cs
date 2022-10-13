#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ContactCampaignCoordinatorStatuses
    {
        [Key]
        public int contact_id { get; set; }
        public short? campaign_coordinator_status_id { get; set; }
        public int? campaign_id { get; set; }
        public DateTime? date_submitted { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
