using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RailwayMVC.Models
{
    public class Saledata
    {
        [Required]
        [StringLength(100)]
        public string CRS_Code { get; set; }

       
        public string Date { get; set; }

        public int? Target { get; set; }

        public int? Actual_val { get; set; }

        public int? Variance { get; set; }
    }
}