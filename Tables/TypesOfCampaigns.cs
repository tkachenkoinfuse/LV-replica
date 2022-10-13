#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class TypesOfCampaigns
    {
        [Key]
        public int id { get; set; }
        [Key]
        public string? short_name { get; set; }
        [Key]
        public string? long_name { get; set; }
        public string? class_name { get; set; }
        public int? order { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
