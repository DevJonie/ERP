﻿// <auto-generated />
using System;
using ERP.SalesOrder.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERP.SalesOrder.Persistence.Migrations
{
    [DbContext(typeof(SaleOrderDbContext))]
    partial class SaleOrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("SalesOrder")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ERP.SalesOrder.Entities.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("SalesOrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("OrderItems", "SalesOrder");
                });

            modelBuilder.Entity("ERP.SalesOrder.Entities.SaleOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SalesOrders", "SalesOrder");
                });

            modelBuilder.Entity("ERP.SalesOrder.Entities.OrderItem", b =>
                {
                    b.HasOne("ERP.SalesOrder.Entities.SaleOrder", "SalesOrder")
                        .WithMany("OrderItems")
                        .HasForeignKey("SalesOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ERP.Common.Money", "UnitPrice", b1 =>
                        {
                            b1.Property<long>("OrderItemId")
                                .HasColumnType("bigint");

                            b1.Property<double>("Amount")
                                .ValueGeneratedOnAdd()
                                .HasPrecision(4)
                                .HasColumnType("float(4)")
                                .HasDefaultValue(0.0)
                                .HasColumnName("UnitPrice");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .ValueGeneratedOnAdd()
                                .HasColumnType("nvarchar(max)")
                                .HasDefaultValue("USD")
                                .HasColumnName("Currency");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems", "SalesOrder");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("SalesOrder");

                    b.Navigation("UnitPrice")
                        .IsRequired();
                });

            modelBuilder.Entity("ERP.SalesOrder.Entities.SaleOrder", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
