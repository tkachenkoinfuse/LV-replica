#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class UserWorkShifts
    {
        [Key]
        public int id { get; set; }
        public string? long_name { get; set; }
        public string? short_name { get; set; }
    }
}
