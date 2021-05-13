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
    [Migration("20210513190245_ExploraTotem_Table_Created")]
    partial class ExploraTotem_Table_Created
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Explora.Domain.ExploraCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("description")
                        .HasMaxLength(250)
                        .HasDefaultValue(null);

                    b.Property<string>("ImageUrl")
                        .HasColumnName("image_url")
                        .HasMaxLength(255);

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

                    b.HasKey("Id");

                    b.ToTable("collections");
                });

            modelBuilder.Entity("Explora.Domain.ExploraFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("CollectionId")
                        .HasColumnName("collection_id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Deleted")
                        .HasColumnName("deleted");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("description")
                        .HasMaxLength(250)
                        .HasDefaultValue(null);

                    b.Property<string>("Extension")
                        .HasColumnName("extension");

                    b.Property<string>("ImageUrl")
                        .HasColumnName("image_url")
                        .HasMaxLength(255);

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

                    b.HasIndex("CollectionId");

                    b.ToTable("files");
                });

            modelBuilder.Entity("Explora.Domain.ExploraTotem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Deleted")
                        .HasColumnName("deleted");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("description")
                        .HasMaxLength(250)
                        .HasDefaultValue(null);

                    b.Property<string>("FileUrl")
                        .HasColumnName("file_url")
                        .HasMaxLength(255);

                    b.Property<string>("ImageUrl")
                        .HasColumnName("image_url")
                        .HasMaxLength(255);

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

                    b.Property<int>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("version")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("totems");
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

            modelBuilder.Entity("Explora.Domain.ExploraFile", b =>
                {
                    b.HasOne("Explora.Domain.ExploraCollection", "Collection")
                        .WithMany("Files")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
