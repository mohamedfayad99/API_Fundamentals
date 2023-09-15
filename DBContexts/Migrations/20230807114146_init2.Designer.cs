﻿// <auto-generated />
using CityInfo.DBContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.DBContexts.Migrations
{
    [DbContext(typeof(ApplicationDb))]
    [Migration("20230807114146_init2")]
    partial class init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CityInfo.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "About first mohamed ibrahim fayad kandil",
                            Name = "Mohamed"
                        },
                        new
                        {
                            Id = 2,
                            Description = "About Two mohamed ibrahim fayad kandil",
                            Name = "Ahmed"
                        },
                        new
                        {
                            Id = 3,
                            Description = "About Thered mohamed ibrahim fayad kandil",
                            Name = "Wallid"
                        });
                });

            modelBuilder.Entity("CityInfo.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cityid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("cityid");

                    b.ToTable("pointOfInterests");

                    b.HasData(
                        new
                        {
                            Id = 200,
                            Description = "About first mohamed ibrahim fayad kandil",
                            Name = "Gamming",
                            cityid = 1
                        },
                        new
                        {
                            Id = 100,
                            Description = "About Second mohamed ibrahim fayad kandil",
                            Name = "Loving",
                            cityid = 1
                        });
                });

            modelBuilder.Entity("CityInfo.Entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.Entities.City", "city")
                        .WithMany("pointOfInterests")
                        .HasForeignKey("cityid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");
                });

            modelBuilder.Entity("CityInfo.Entities.City", b =>
                {
                    b.Navigation("pointOfInterests");
                });
#pragma warning restore 612, 618
        }
    }
}