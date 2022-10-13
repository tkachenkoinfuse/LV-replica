#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ContactTitles
    {
        [Key]
        public int id { get; set; }
        public int? contact_id { get; set; }
        public string? title { get; set; }
        public short? report { get; set; }
        public short? db_main { get; set; }
    }
}
