#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCStrikeComments
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public string? entity { get; set; }
        public short? deprecated { get; set; }
    }
}
