﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User1.Entities;

#nullable disable

namespace User1.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("User1.Entities.User", b =>
                {
                    b.Property<Guid>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("userTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            userId = new Guid("18a38124-3eb4-44dc-941a-1d164661b615"),
                            name = "Una",
                            password = "123456",
                            salt = "",
                            surname = "Obradovic",
                            userTypeId = new Guid("95d791cc-a0c9-4ebd-a598-9ccad0022a78"),
                            username = "UUna"
                        },
                        new
                        {
                            userId = new Guid("90fa6cde-79e2-4b82-b0a5-28d70c66e2dd"),
                            name = "Dusan",
                            password = "123456",
                            salt = "",
                            surname = "Markovic",
                            userTypeId = new Guid("50d0d37e-b01a-4d48-aa12-be3acc5cf379"),
                            username = "MMarkovic"
                        });
                });

            modelBuilder.Entity("User1.Entities.UserType", b =>
                {
                    b.Property<Guid>("userTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userTypeId");

                    b.ToTable("UserType");

                    b.HasData(
                        new
                        {
                            userTypeId = new Guid("17f97a34-89b3-48fa-a6c0-265d15a18d3c"),
                            role = "Admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}