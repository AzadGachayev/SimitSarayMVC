namespace Simit_Saray.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SimitSarayDB : DbContext
    {
        public SimitSarayDB()
            : base("name=SimitSarayDB")
        {
        }

        public virtual DbSet<About> About { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<BlogItem> BlogItem { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<Gallery2> Gallery2 { get; set; }
        public virtual DbSet<BlogPhoto> BlogPhoto { get; set; }
        public virtual DbSet<HomeHeader> HomeHeader { get; set; }
        public virtual DbSet<HomeNavbar> HomeNavbar { get; set; }
        public virtual DbSet<MenuCategory> MenuCategory { get; set; }
        public virtual DbSet<Menyu> Menyu { get; set; }
        public virtual DbSet<Profil> Profil { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogItem>()
                .Property(e => e.Header)
                .IsFixedLength();

            modelBuilder.Entity<MenuCategory>()
                .HasMany(e => e.Menyu)
                .WithOptional(e => e.MenuCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Profil>()
                .HasMany(e => e.Reservation)
                .WithOptional(e => e.Profil)
                .HasForeignKey(e => e.UserID);
        }
    }
}
