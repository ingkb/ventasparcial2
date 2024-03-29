﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ventas.infrastructure;

namespace WebApp.Migrations
{
    [DbContext(typeof(ventasContext))]
    [Migration("20210504001814_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ventas.domain.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Costo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductoPadreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductoPadreId");

                    b.ToTable("Productos");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Producto");
                });

            modelBuilder.Entity("ventas.domain.ProductoCompuesto", b =>
                {
                    b.HasBaseType("ventas.domain.Producto");

                    b.HasDiscriminator().HasValue("ProductoCompuesto");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Codigo = "004",
                            Costo = 1500m,
                            Nombre = "Perro",
                            Precio = 5000m
                        });
                });

            modelBuilder.Entity("ventas.domain.ProductoSimple", b =>
                {
                    b.HasBaseType("ventas.domain.Producto");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("ProductoSimple");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Codigo = "001",
                            Costo = 500m,
                            Nombre = "Pan",
                            Precio = 0m,
                            ProductoPadreId = 4,
                            Cantidad = 10
                        },
                        new
                        {
                            Id = 2,
                            Codigo = "002",
                            Costo = 1000m,
                            Nombre = "Salchicha",
                            Precio = 0m,
                            ProductoPadreId = 4,
                            Cantidad = 10
                        },
                        new
                        {
                            Id = 3,
                            Codigo = "003",
                            Costo = 1000m,
                            Nombre = "Cocacola",
                            Precio = 3000m,
                            Cantidad = 10
                        });
                });

            modelBuilder.Entity("ventas.domain.Producto", b =>
                {
                    b.HasOne("ventas.domain.ProductoCompuesto", "ProductoPadre")
                        .WithMany("Productos")
                        .HasForeignKey("ProductoPadreId");

                    b.Navigation("ProductoPadre");
                });

            modelBuilder.Entity("ventas.domain.ProductoCompuesto", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
