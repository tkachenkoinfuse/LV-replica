#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class TimeIntervalsHour
    {
        [Key]
        public int? user_id { get; set; }
        [Key]
        public DateTime day { get; set; }
        [Key]
        public int hour { get; set; }
        public DateTime? user_started { get; set; }
        public DateTime? user_ended { get; set; }
        public int? leads_count { get; set; }
        public int? seconds_platform { get; set; }
    }
}
