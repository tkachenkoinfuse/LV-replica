#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class UserSchedules
    {
        [Key]
        public int user_id { get; set; }
        public DateTime day { get; set; }
        public DateTime? day_end { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public bool? manual { get; set; }
    }
}
