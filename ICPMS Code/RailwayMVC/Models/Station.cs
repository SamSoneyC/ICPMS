namespace RailwayMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Station")]
    public partial class Station
    {
        [StringLength(100)]
        public string Station_Name { get; set; }

        [Key]
        [StringLength(100)]
        public string CRS_Code { get; set; }

        public int? Target { get; set; }
    }
}
