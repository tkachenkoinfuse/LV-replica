#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class Rejects
    {
        [Key]
        public int id { get; set; }
        public int? list_id { get; set; }
        public int? rejected { get; set; }
        public int? title_green { get; set; }
        public int? title_yellow { get; set; }
        public int? country_green { get; set; }
        public int? country_yellow { get; set; }
        public int? industry_green { get; set; }
        public int? industry_yellow { get; set; }
        public int? employees_green { get; set; }
        public int? employees_yellow { get; set; }
        public int? revenue_green { get; set; }
        public int? revenue_yellow { get; set; }
        public int? nac_sup_company { get; set; }
        public int? nac_sup_contact { get; set; }
        public int? q_title { get; set; }
        public int? q_company { get; set; }
        public int? na_proof { get; set; }
        public int? na_other_contact { get; set; }
        public int? na_other_company { get; set; }
        public int? na_duplicate { get; set; }
        public int? nwc_ov { get; set; }
        public int? bad_data { get; set; }
        public int? out_of_business { get; set; }
        public int? nwc_pv { get; set; }
    }
}
