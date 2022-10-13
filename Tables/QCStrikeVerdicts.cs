#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCStrikeVerdicts
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
    }
}
