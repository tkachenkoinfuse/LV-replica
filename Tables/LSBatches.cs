#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class LSBatches
    {
        [Key]
        public int id { get; set; }
        public int? ls_batch_id { get; set; }
        public short? status_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? main_cid { get; set; }
        public string? sub_cid { get; set; }
        public string? reason { get; set; }
    }
}
