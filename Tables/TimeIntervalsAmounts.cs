#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class TimeIntervalsAmounts
    {
        [Key]
        public int id { get; set; }
        public int? contact_id { get; set; }
        public int? user_id { get; set; }
        public int? list_id { get; set; }
        public int? time_contact { get; set; }
        public DateTime? date { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
