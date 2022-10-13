#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class PhoneReasons
    {
        [Key]
        public int id { get; set; }
        public string? key_name { get; set; }
        public string? phone_reason { get; set; }
        public string? short_name { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public short? show { get; set; }
        public string? sign { get; set; }
        
    }
}
