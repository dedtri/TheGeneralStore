﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheGeneralStore.Backend.Database;

#nullable disable

namespace TheGeneralStore.Backend.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ProductId");

                    b.ToTable("carts");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FileExtension")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ProductId");

                    b.ToTable("images");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Role")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("logins");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Total_Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("orderproducts");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("products");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Cart", b =>
                {
                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Customer", "Customer")
                        .WithMany("Carts")
                        .HasForeignKey("CustomerId");

                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Image", b =>
                {
                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Order", b =>
                {
                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.OrderProduct", b =>
                {
                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Product", b =>
                {
                    b.HasOne("TheGeneralStore.Backend.Database.DataModels.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Customer", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("TheGeneralStore.Backend.Database.DataModels.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Images");

                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
