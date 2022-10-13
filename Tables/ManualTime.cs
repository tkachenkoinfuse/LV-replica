#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class ManualTime
    {
        [Key]
        public int record_id { get; set; }
        public int? minutes { get; set; }

        
    }
}
