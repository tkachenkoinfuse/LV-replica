#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ContactCountries
    {
        [Key]
        public int id { get; set; }
        public int? contact_id { get; set; }
        public int? country_id { get; set; }
        public string? country { get; set; }
    }
}
