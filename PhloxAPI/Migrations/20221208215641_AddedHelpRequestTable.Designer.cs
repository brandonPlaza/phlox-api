﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhloxAPI.Data;

#nullable disable

namespace PhloxAPI.Migrations
{
    [DbContext(typeof(PhloxDbContext))]
    [Migration("20221208215641_AddedHelpRequestTable")]
    partial class AddedHelpRequestTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ConnectedBuildingId");

                    b.HasIndex("UserId");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("PhloxAPI.Models.Building", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConnectedBuilding")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Letter")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("PhloxAPI.Models.HelpRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitute")
                        .HasColumnType("float");

                    b.Property<int?>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeAccepted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeCancelled")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeCompleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HelpRequests");
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

                    b.HasOne("PhloxAPI.Models.User", null)
                        .WithMany("FavouriteAmenities")
                        .HasForeignKey("UserId");

                    b.Navigation("ConnectedBuilding");
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

            modelBuilder.Entity("PhloxAPI.Models.User", b =>
                {
                    b.Navigation("FavouriteAmenities");
                });
#pragma warning restore 612, 618
        }
    }
}
