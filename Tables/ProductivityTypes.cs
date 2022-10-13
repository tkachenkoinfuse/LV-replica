#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ProductivityTypes
    {
        [Key]
        public int id { get; set; }
        public string? type { get; set; }
        
    }
}
