#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class LSBatchStatuses
    {
        [Key]
        public int id { get; set; }
        public string? short_name { get; set; }
        public string? long_name { get; set; }
        public string? class_name { get; set; }

    }
}
