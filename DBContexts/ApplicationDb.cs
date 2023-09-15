using CityInfo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.DBContexts
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options):base(options)
        {

        }
        public DbSet<City> cities { get; set; }
        public DbSet<PointOfInterest> pointOfInterests { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .EnableSensitiveDataLogging();
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<City>().HasData(
        //        new City("Mohamed")
        //        {
        //            Id = 1,
        //            Description = "About first mohamed ibrahim fayad kandil"
        //        },
        //        new City("Ahmed")
        //        {
        //            Id = 2,
        //            Description = "About Two mohamed ibrahim fayad kandil"
        //        },
        //        new City("Wallid")
        //        {
        //            Id = 3,
        //            Description = "About Thered mohamed ibrahim fayad kandil"
        //        });
        //    modelBuilder.Entity<PointOfInterest>().HasData(
        //        new PointOfInterest("Gamming")
        //        {
        //            Id = 200,
        //            cityid = 1,
        //            Description = "About first mohamed ibrahim fayad kandil"
        //        },
        //        new PointOfInterest("Loving")
        //        {
        //            Id = 100,
        //            cityid = 1,
        //            Description = "About Second mohamed ibrahim fayad kandil"
        //        });
        //}
    }
}
