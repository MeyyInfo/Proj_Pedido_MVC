﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proj_Pedido_MVC.Data;

namespace Proj_Pedido_MVC.Migrations
{
    [DbContext(typeof(Proj_Pedido_MVCContext))]
    [Migration("20200515143649_OtherEntities")]
    partial class OtherEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Proj_Pedido_MVC.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Proj_Pedido_MVC.Models.SalesRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<int>("SalesStatus");

                    b.Property<int?>("SellerId");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("SalesRecord");
                });

            modelBuilder.Entity("Proj_Pedido_MVC.Models.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BaseSalary");

                    b.Property<DateTime>("BirthDate");

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("Proj_Pedido_MVC.Models.SalesRecord", b =>
                {
                    b.HasOne("Proj_Pedido_MVC.Models.Seller", "Seller")
                        .WithMany("List_SalesRecord")
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("Proj_Pedido_MVC.Models.Seller", b =>
                {
                    b.HasOne("Proj_Pedido_MVC.Models.Department", "Department")
                        .WithMany("List_Seller")
                        .HasForeignKey("DepartmentId");
                });
#pragma warning restore 612, 618
        }
    }
}
