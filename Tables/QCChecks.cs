#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class QCChecks
    {
        [Key]
        public int id { get; set; }
        public int? entity_id { get; set; }
        public string? entity_type { get; set; }
        public int? contact_id { get; set; }
        public int? list_id { get; set; }
        public int? user_id { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
        public int? is_earliest { get; set; }
        public int? is_latest { get; set; }
        public int? was_earliest { get; set; }
        public int? was_latest { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? date { get; set; }
    }
}
