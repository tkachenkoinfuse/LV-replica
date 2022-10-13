#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ManualVerifications
    {
        [Key]
        public int record_id { get; set; }
        public int? ov { get; set; }
        public int? pv { get; set; }
        public float? ov_mistakes { get; set; }
        public float? pv_mistakes { get; set; }

        
    }
}
