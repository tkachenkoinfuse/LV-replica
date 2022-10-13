#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class LSLeads
    {
        [Key]
        public int id { get; set; }
        public int? contact_id { get; set; }
        public int? batch_id { get; set; }
        public short? status_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? reason { get; set; }
    }
}
