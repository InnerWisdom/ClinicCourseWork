﻿// <auto-generated />
using System;
using ClinicDatabaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClinicDatabaseImplement.Migrations
{
    [DbContext(typeof(ClinicDataBase))]
    [Migration("20210520203924_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Difficulty")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("F_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("L_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.DrugCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FormedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int?>("MedicineId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MedicineId1");

                    b.ToTable("DrugCourses");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.DrugCourseDisease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("DrugCourseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("DrugCourseId");

                    b.ToTable("DrugCoursesDiseases");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrugCourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MedicineId")
                        .HasColumnType("int");

                    b.Property<int>("PerDose")
                        .HasColumnType("int");

                    b.Property<int?>("SymptomaticsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Symptomatic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Heart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Liver")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lungs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<string>("SymptomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Symptomatics");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.SymptomaticDisease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("SymptomaticId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("SymptomaticId");

                    b.ToTable("SymptomaticDiseases");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Disease", b =>
                {
                    b.HasOne("ClinicDatabaseImplement.Models.Doctor", "Doctor")
                        .WithMany("Disease")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.DrugCourse", b =>
                {
                    b.HasOne("ClinicDatabaseImplement.Models.Doctor", "Doctor")
                        .WithMany("DrugCourse")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicDatabaseImplement.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId1");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.DrugCourseDisease", b =>
                {
                    b.HasOne("ClinicDatabaseImplement.Models.Disease", "Disease")
                        .WithMany()
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicDatabaseImplement.Models.DrugCourse", "DrugCourse")
                        .WithMany("DrugCourseDiseases")
                        .HasForeignKey("DrugCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.Symptomatic", b =>
                {
                    b.HasOne("ClinicDatabaseImplement.Models.Doctor", "Doctor")
                        .WithMany("Symptomatic")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicDatabaseImplement.Models.Receipt", "Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptId");
                });

            modelBuilder.Entity("ClinicDatabaseImplement.Models.SymptomaticDisease", b =>
                {
                    b.HasOne("ClinicDatabaseImplement.Models.Disease", "Disease")
                        .WithMany()
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClinicDatabaseImplement.Models.Symptomatic", null)
                        .WithMany("SymptomaticDiseases")
                        .HasForeignKey("SymptomaticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}