﻿// <auto-generated />
using System;
using CompanyManager.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompanyManager.Infrastructure.Data.EntityFramework.Migrations
{
    [DbContext(typeof(CompaniesDbContext))]
    partial class CompaniesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.CompanyDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.DepartmentDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.DivisionDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.DepartmentDto", b =>
                {
                    b.HasOne("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.CompanyDto", "Company")
                        .WithMany("Departments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.DivisionDto", b =>
                {
                    b.HasOne("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.DepartmentDto", "Department")
                        .WithMany("Divisions")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.CompanyDto", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("CompanyManager.Infrastructure.Data.EntityFramework.DTOs.DepartmentDto", b =>
                {
                    b.Navigation("Divisions");
                });
#pragma warning restore 612, 618
        }
    }
}
