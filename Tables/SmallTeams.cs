#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceWithHangfire
{
    public class SmallTeams
    {
        [Key]
        public int id { get; set; }
        public int? owner_id { get; set; }
        public string? name { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
