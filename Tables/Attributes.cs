#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Attributes
    {
        [Key]
        public int id { get; set; }
        public string? key { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
