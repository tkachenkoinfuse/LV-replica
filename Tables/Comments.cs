#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceWithHangfire
{
    public class Comments
    {
        [Key]
        public int id { get; set; }
        public int? version_id { get; set; }
        public int? entity_id { get; set; }
        public string? comment { get; set; }
        public string? type { get; set; }
        public int? amended_by { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string? description { get; set; }
    }
}
