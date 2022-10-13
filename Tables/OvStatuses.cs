#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class OvStatuses
    {
        [Key]
        public int id { get; set; }
        public string? short_name { get; set; }
        public string? long_name { get; set; }
        public string? long_name_for_export { get; set; }
        public string? full_name { get; set; }
        public string? text_for_contact { get; set; }
        public string? text_for_company { get; set; }
        public string? pv_availability { get; set; }
        public string? color_cod_for_contact { get; set; }
        public string? color_cod_for_company { get; set; }
        public string? description { get; set; }
        public int? order { get; set; }
        public string? sign { get; set; }
        public int? self_result_sort { get; set; }
        public string? dc_sign { get; set; }

    }
}
