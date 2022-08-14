namespace RailwayMVC.NewFolder1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public int User_id { get; set; }

        [StringLength(200)]
        public string User_name { get; set; }

        [StringLength(100)]
        public string Pass { get; set; }
    }
}
