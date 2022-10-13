#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCCheckReactions
    {
        [Key]
        public int id { get; set; }
        public int? qc_check_id { get; set; }
        public int? user_id { get; set; }
        public string? reaction { get; set; }
        public int? is_latest { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
