#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCStrikeReasons
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? abbreviation { get; set; }
        public short? deprecated { get; set; }
    }
}
