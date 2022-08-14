using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RailwayMVC.Models
{
    public partial class MVCContext : DbContext
    {
        public MVCContext()
            : base("name=Modelnew")
        {
        }

        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Station> Stations { get; set; }

        public virtual DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .Property(e => e.CRS_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Station_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.CRS_Code)
                .IsUnicode(false);
        }
    }
}
