#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class UserExpectedWorkingTime
    {
        [Key]
        public int id { get; set; }
        public int? user_id { get; set; }
        public DateTime? date { get; set; }
        public int? minutes { get; set; }
        public int? minutes_absent { get; set; }
        public int? minutes_meeting { get; set; }
    }
}
