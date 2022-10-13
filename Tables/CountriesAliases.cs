#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class CountriesAliases
    {
        [Key]
        public int id { get; set; }
        public int? country_id { get; set; }
        public string? alias { get; set; }
    }
}
