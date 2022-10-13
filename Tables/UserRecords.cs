#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class UserRecords
    {
        [Key]
        public int id { get; set; }
        public int? user_id { get; set; }
        public DateTime? date { get; set; }
        public int? productivity_type_id { get; set; }
    }
}
