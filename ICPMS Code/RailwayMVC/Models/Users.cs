using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RailwayMVC.Models
{
    public class Users
    {
        [Key]
        public int User_id { get; set; }
        [Required]
        [StringLength(200)]
        public string User_name { get; set; }
        [Required]
        public string Pass { get; set; }




    }
}