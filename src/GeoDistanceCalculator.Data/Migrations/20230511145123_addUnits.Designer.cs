﻿// <auto-generated />
using System;
using GeoDistanceCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeoDistanceCalculator.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230511145123_addUnits")]
    partial class addUnits
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("GeoDistanceCalculator.Domain.Models.Coordinate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coordinates");
                });

            modelBuilder.Entity("GeoDistanceCalculator.Domain.Models.CoordinateCalculation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Coordinate1Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Coordinate2Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<double>("Distance")
                        .HasColumnType("REAL");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Coordinate1Id");

                    b.HasIndex("Coordinate2Id");

                    b.ToTable("CoordinateCalculations");
                });

            modelBuilder.Entity("GeoDistanceCalculator.Domain.Models.CoordinateCalculation", b =>
                {
                    b.HasOne("GeoDistanceCalculator.Domain.Models.Coordinate", "Coordinate1")
                        .WithMany()
                        .HasForeignKey("Coordinate1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GeoDistanceCalculator.Domain.Models.Coordinate", "Coordinate2")
                        .WithMany()
                        .HasForeignKey("Coordinate2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coordinate1");

                    b.Navigation("Coordinate2");
                });
#pragma warning restore 612, 618
        }
    }
}
