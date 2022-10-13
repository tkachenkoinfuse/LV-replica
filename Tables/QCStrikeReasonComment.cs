#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCStrikeReasonComment
    {
        [Key]
        public int id { get; set; }
        public int? qc_strike_reason_id { get; set; }
        public int? qc_strike_comment_id { get; set; }
    }
}
