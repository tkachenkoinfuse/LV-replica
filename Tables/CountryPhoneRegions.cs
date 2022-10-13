#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CountryPhoneRegions
    {
        [Key]
        public int id { get; set; }
        public int? country_id { get; set; }
        public string? region_code { get; set; }
        public string? region_name { get; set; }
    }
}
