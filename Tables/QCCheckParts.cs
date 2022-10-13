#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCCheckParts
    {
        [Key]
        public int id { get; set; }
        public int? qc_check_id { get; set; }
        public int? user_id { get; set; }
        public DateTime? date { get; set; }
        public int? verification_id { get; set; }
        public int? comment_id { get; set; }
        public string? comment_type { get; set; }
        public int? has_strike { get; set; }
        public string? strike_entity { get; set; }
        public float? strike_weight { get; set; }
        public int? strike_reason_id { get; set; }
        public int? strike_comment_id { get; set; }
        public int? strike_verdict_id { get; set; }

    }
}
