﻿// <auto-generated />
using System;
using Explora.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Explora.DataLayer.Migrations
{
    [DbContext(typeof(ExploraContext))]
    [Migration("20200608195358_ExploraFile_PlatformColumnAdded")]
    partial class ExploraFile_PlatformColumnAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Explora.Domain.ExploraFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Extension")
                        .HasColumnName("extension");

                    b.Property<DateTime>("LastTimeStamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("last_timestamp")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnName("modified_date")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("name")
                        .HasMaxLength(100)
                        .HasDefaultValue(null);

                    b.Property<int>("Platform")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("platform")
                        .HasDefaultValue(1);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnName("url")
                        .HasMaxLength(255);

                    b.Property<int>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("version")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("files");
                });

            modelBuilder.Entity("Explora.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("last_name")
                        .HasMaxLength(50)
                        .HasDefaultValue(null);

                    b.Property<DateTime>("LastTimeStamp")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("last_timestamp")
                        .HasColumnType("timestamp");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("name")
                        .HasMaxLength(50)
                        .HasDefaultValue(null);

                    b.Property<string>("SecondLastName")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("second_last_name")
                        .HasMaxLength(50)
                        .HasDefaultValue(null);

                    b.HasKey("Id");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
