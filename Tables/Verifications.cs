#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Verifications
    {
        [Key]
        public int id { get; set; }
        public int? prev_id { get; set; }
        public int? entity_id { get; set; }
        public int? user_id { get; set; }
        public string? entity { get; set; }
        public string? method { get; set; }
        public int? status { get; set; }
        public int? status_pv { get; set; }
        public int? ov_status { get; set; }
        public int? pv_comment_id { get; set; }
        public int? comment_id { get; set; }
        public int? latest { get; set; }
        public string? not_amendable { get; set; }
        public int? score_ov { get; set; }
        public int? score_pv { get; set; }
        public ulong? uuid { get; set; }
        public int? batch_id { get; set; }
        public int? overwrite_batch_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? is_latest_score { get; set; }
        public int? is_recheck_score { get; set; }
        public DateTime? processed_at { get; set; }
        public int? latest_score_from_ov { get; set; }
        public int? latest_score_from_pv { get; set; }
        public DateTime? date { get; set; }
        public int? hour { get; set; }
    }
}
