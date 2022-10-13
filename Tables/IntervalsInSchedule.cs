#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class IntervalsInSchedule
    {
        [Key]
        public int user_id { get; set; }
        [Key]
        public DateTime day { get; set; }
        [Key]
        public short? hour { get; set; }
        [Key]
        public int? type_id { get; set; }
        public DateTime? interval_start { get; set; }
        public DateTime? interval_end { get; set; }
        public int? leads_count { get; set; }
        public int? seconds_total { get; set; }
        public int? result_time { get; set; }
        public int? manual_time { get; set; }
        public int? seconds_platform { get; set; }
        public float? coeff_fact { get; set; }
        public int? score_ov { get; set; }
        public int? score_pv { get; set; }
        public float? ov_mistakes { get; set; }
        public float? pv_mistakes { get; set; }
        public int? ov_qc_checked { get; set; }
        public int? pv_qc_checked { get; set; }
    }
}
