#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class UserAttributes
    {
        [Key]
        public int id { get; set; }
        public int? user_id { get; set; }
        public int? attribute_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
