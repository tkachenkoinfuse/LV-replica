#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class MetaFields
    {
        [Key]
        public int id { get; set; }
        public int? entity_id { get; set; }
        public string? entity_type { get; set; }
        public string? field { get; set; }
        public short? meet { get; set; }
        public short? change { get; set; }
        public int? sub_id { get; set; }
        public DateTime? date { get; set; }
       
    }
}
