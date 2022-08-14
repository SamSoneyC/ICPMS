namespace RailwayMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        [Required]
        [StringLength(100)]
        public string CRS_Code { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int? Target { get; set; }

        public int? Actual_val { get; set; }
        


        [Key]
        public int PK_ID { get; set; }
    }
}
