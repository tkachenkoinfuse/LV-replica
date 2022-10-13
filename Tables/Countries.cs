#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Countries
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public short? group { get; set; }
        public int? contacts_num { get; set; }
        public int? phone_code { get; set; }
        public string? short_name { get; set; }
        public string? country_code { get; set; }
        public short? has_state { get; set; }
        public short? top { get; set; }
    }
}
