﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using complaint.Entities;

#nullable disable

namespace complaint.Migrations
{
    [DbContext(typeof(ComplaintContext))]
    partial class ComplaintContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("complaint.Entities.Action", b =>
                {
                    b.Property<Guid>("actionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("actionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("actionId");

                    b.ToTable("Action");

                    b.HasData(
                        new
                        {
                            actionId = new Guid("0ff49176-03ff-4e8e-9878-038a56e35a5b"),
                            actionName = "JN ide u drugi krug sa novim uslovima"
                        },
                        new
                        {
                            actionId = new Guid("df859a22-1ce8-466c-b919-f4cfbea3c7a6"),
                            actionName = "JN ide u drugi krug sa starim uslovima"
                        },
                        new
                        {
                            actionId = new Guid("228c0094-41ed-4455-bd11-0f024dd199e9"),
                            actionName = "JN ne ide u drugi krug"
                        });
                });

            modelBuilder.Entity("complaint.Entities.Complaint", b =>
                {
                    b.Property<Guid>("complaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("actionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("buyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("cause")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("complaintDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("complaintNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("complaintStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("complaintSubmitter")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("complaintTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("decisionNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("rescriptDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("rescriptNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("complaintId");

                    b.HasIndex("actionId");

                    b.HasIndex("complaintStatusId");

                    b.HasIndex("complaintTypeId");

                    b.ToTable("Complaint");

                    b.HasData(
                        new
                        {
                            complaintId = new Guid("a6c49ae9-75f8-4685-8671-b74cc94ebfc0"),
                            actionId = new Guid("0ff49176-03ff-4e8e-9878-038a56e35a5b"),
                            buyerId = new Guid("75f86fa2-a650-47e6-975f-dfe7276a92f7"),
                            cause = "Krsenje pravilnika za javno nadmetanje",
                            complaintDate = new DateTime(2023, 2, 15, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            complaintNumber = "1234",
                            complaintStatusId = new Guid("5c416d45-715a-4432-b2b6-2df9046fe828"),
                            complaintSubmitter = new Guid("702e05d2-afea-48b0-a8ae-48ac259915c1"),
                            complaintTypeId = new Guid("f98de9dc-5a4a-4ee2-bccc-fba4134dd97a"),
                            decisionNumber = "1221",
                            reason = "Neispravnost prilikom dodeljivanja parcele",
                            rescriptDate = new DateTime(2023, 3, 11, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            rescriptNumber = "1035"
                        });
                });

            modelBuilder.Entity("complaint.Entities.ComplaintStatus", b =>
                {
                    b.Property<Guid>("complaintStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("statusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("complaintStatusId");

                    b.ToTable("ComplaintStatus");

                    b.HasData(
                        new
                        {
                            complaintStatusId = new Guid("5c416d45-715a-4432-b2b6-2df9046fe828"),
                            statusName = "Usvojena"
                        },
                        new
                        {
                            complaintStatusId = new Guid("436b9e51-057a-404d-ab52-155a2b4d8071"),
                            statusName = "Odbijena"
                        },
                        new
                        {
                            complaintStatusId = new Guid("02b27d09-4958-4245-be2c-76e434e39351"),
                            statusName = "Otvorena"
                        });
                });

            modelBuilder.Entity("complaint.Entities.ComplaintType", b =>
                {
                    b.Property<Guid>("complaintTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("typeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("complaintTypeId");

                    b.ToTable("ComplaintType");

                    b.HasData(
                        new
                        {
                            complaintTypeId = new Guid("f98de9dc-5a4a-4ee2-bccc-fba4134dd97a"),
                            typeName = "Žalba na tok javnog nadmetanja"
                        },
                        new
                        {
                            complaintTypeId = new Guid("53b25384-45c5-4f30-8b27-3db311e855fb"),
                            typeName = "Žalba na Odluku o davanju u zakup"
                        },
                        new
                        {
                            complaintTypeId = new Guid("071849eb-3561-40fe-9dcf-1f57fa7f6ff8"),
                            typeName = "Žalba na Odluku o davanju na korišćenje"
                        });
                });

            modelBuilder.Entity("complaint.Entities.Complaint", b =>
                {
                    b.HasOne("complaint.Entities.Action", "action")
                        .WithMany()
                        .HasForeignKey("actionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("complaint.Entities.ComplaintStatus", "complaintStatus")
                        .WithMany()
                        .HasForeignKey("complaintStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("complaint.Entities.ComplaintType", "complaintType")
                        .WithMany()
                        .HasForeignKey("complaintTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("action");

                    b.Navigation("complaintStatus");

                    b.Navigation("complaintType");
                });
#pragma warning restore 612, 618
        }
    }
}
