﻿// <auto-generated />
using System;
using EDNETLMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EDNETLMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240605023927_AddAddress")]
    partial class AddAddress
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EDNETLMS.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryID"));

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("EDNETLMS.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseID"));

                    b.Property<string>("CourseDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseID");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("EDNETLMS.Models.Institution", b =>
                {
                    b.Property<int>("InstitutionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstitutionID"));

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("InstitutionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstitutionID");

                    b.HasIndex("CountryID");

                    b.ToTable("Institutions", (string)null);
                });

            modelBuilder.Entity("EDNETLMS.Models.Lead", b =>
                {
                    b.Property<int>("LeadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeadID"));

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("HowDoYouKnowUs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LeadStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeadID");

                    b.HasIndex("CourseID");

                    b.HasIndex("PersonID");

                    b.ToTable("Leads", (string)null);
                });

            modelBuilder.Entity("EDNETLMS.Models.LeadCatchUpStatus", b =>
                {
                    b.Property<int>("LeadCatchUpStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeadCatchUpStatusID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DoneDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeadCatchUpRemark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeadID")
                        .HasColumnType("int");

                    b.Property<string>("LeadStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LeadCatchUpStatusID");

                    b.HasIndex("LeadID");

                    b.ToTable("LeadCatchUpStatus", (string)null);
                });

            modelBuilder.Entity("EDNETLMS.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EducationLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GivenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstitutionID")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.HasIndex("InstitutionID");

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("EDNETLMS.Models.Institution", b =>
                {
                    b.HasOne("EDNETLMS.Models.Country", "Country")
                        .WithMany("Institutions")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("EDNETLMS.Models.Lead", b =>
                {
                    b.HasOne("EDNETLMS.Models.Course", "Course")
                        .WithMany("Leads")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDNETLMS.Models.Person", "Person")
                        .WithMany("Leads")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("EDNETLMS.Models.LeadCatchUpStatus", b =>
                {
                    b.HasOne("EDNETLMS.Models.Lead", "Lead")
                        .WithMany("LeadCatchUpStatuses")
                        .HasForeignKey("LeadID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lead");
                });

            modelBuilder.Entity("EDNETLMS.Models.Person", b =>
                {
                    b.HasOne("EDNETLMS.Models.Institution", "Institution")
                        .WithMany("Persons")
                        .HasForeignKey("InstitutionID");

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("EDNETLMS.Models.Country", b =>
                {
                    b.Navigation("Institutions");
                });

            modelBuilder.Entity("EDNETLMS.Models.Course", b =>
                {
                    b.Navigation("Leads");
                });

            modelBuilder.Entity("EDNETLMS.Models.Institution", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("EDNETLMS.Models.Lead", b =>
                {
                    b.Navigation("LeadCatchUpStatuses");
                });

            modelBuilder.Entity("EDNETLMS.Models.Person", b =>
                {
                    b.Navigation("Leads");
                });
#pragma warning restore 612, 618
        }
    }
}
