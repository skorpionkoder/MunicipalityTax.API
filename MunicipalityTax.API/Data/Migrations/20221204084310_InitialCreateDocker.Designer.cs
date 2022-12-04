﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MunicipalityTax.API.Data.Contexts;

#nullable disable

namespace MunicipalityTax.API.Migrations
{
    [DbContext(typeof(MunicipalityTaxContext))]
    [Migration("20221204084310_InitialCreateDocker")]
    partial class InitialCreateDocker
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MunicipalityTax.API.Data.Entities.Municipality", b =>
                {
                    b.Property<int>("MunicipalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MunicipalityId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MunicipalityId");

                    b.ToTable("Municipality");
                });

            modelBuilder.Entity("MunicipalityTax.API.Data.Entities.MunicipalityTax", b =>
                {
                    b.Property<int>("MunicipalityTaxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MunicipalityTaxId"));

                    b.Property<DateTime>("EndDtm")
                        .HasColumnType("datetime2");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDtm")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TaxAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TaxTypeId")
                        .HasColumnType("int");

                    b.HasKey("MunicipalityTaxId");

                    b.HasIndex("MunicipalityId");

                    b.HasIndex("TaxTypeId");

                    b.ToTable("MunicipalityTax");
                });

            modelBuilder.Entity("MunicipalityTax.API.Data.Entities.TaxType", b =>
                {
                    b.Property<int>("TaxTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxTypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaxTypeId");

                    b.ToTable("TaxType");
                });

            modelBuilder.Entity("MunicipalityTax.API.Data.Entities.MunicipalityTax", b =>
                {
                    b.HasOne("MunicipalityTax.API.Data.Entities.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MunicipalityTax.API.Data.Entities.TaxType", "TaxType")
                        .WithMany()
                        .HasForeignKey("TaxTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");

                    b.Navigation("TaxType");
                });
#pragma warning restore 612, 618
        }
    }
}
