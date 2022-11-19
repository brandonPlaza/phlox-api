﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhloxAPI.Data;

#nullable disable

namespace PhloxAPI.Migrations
{
    [DbContext(typeof(PhloxDbContext))]
    partial class PhloxDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PhloxAPI.Models.Amenity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<Guid>("ConnectedBuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<bool>("IsOutOfService")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConnectedBuildingId");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("PhloxAPI.Models.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Letter")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("PhloxAPI.Models.Report", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AmenityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AmenityId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("PhloxAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PhloxAPI.Models.Amenity", b =>
                {
                    b.HasOne("PhloxAPI.Models.Building", "ConnectedBuilding")
                        .WithMany()
                        .HasForeignKey("ConnectedBuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConnectedBuilding");
                });

            modelBuilder.Entity("PhloxAPI.Models.Building", b =>
                {
                    b.HasOne("PhloxAPI.Models.Building", null)
                        .WithMany("ConnectedBuildings")
                        .HasForeignKey("BuildingId");
                });

            modelBuilder.Entity("PhloxAPI.Models.Report", b =>
                {
                    b.HasOne("PhloxAPI.Models.Amenity", "Amenity")
                        .WithMany("Reports")
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Amenity");
                });

            modelBuilder.Entity("PhloxAPI.Models.Amenity", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("PhloxAPI.Models.Building", b =>
                {
                    b.Navigation("ConnectedBuildings");
                });
#pragma warning restore 612, 618
        }
    }
}
