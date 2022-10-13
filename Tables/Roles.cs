#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Roles
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? abbreviation { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? guard_name { get; set; }
    }
}
