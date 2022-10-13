#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class UniqueRejects
    {
        [Key]
        public int id { get; set; }
        public int? list_id { get; set; }
        public int? industry_green { get; set; }
        public int? industry_yellow { get; set; }
        public int? employees_green { get; set; }
        public int? employees_yellow { get; set; }
        public int? revenue_green { get; set; }
        public int? revenue_yellow { get; set; }
        public int? nac_sup_company { get; set; }
        public int? q_company { get; set; }
        public int? na_other_company { get; set; }
        public int? out_of_business { get; set; }
    }
}
