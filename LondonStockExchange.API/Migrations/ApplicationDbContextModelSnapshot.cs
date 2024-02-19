﻿// <auto-generated />
using System;
using LondonStockExchange.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LondonStockExchange.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LondonStockExchange.API.Models.Broker", b =>
                {
                    b.Property<int>("BrokerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrokerID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrokerID");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("LondonStockExchange.API.Models.Stock", b =>
                {
                    b.Property<int>("StockID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StockID"));

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("TickerSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StockID");

                    b.HasIndex("TickerSymbol")
                        .IsUnique();

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("LondonStockExchange.API.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionID"));

                    b.Property<int>("BrokerID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("Shares")
                        .HasColumnType("decimal(18, 4)");

                    b.Property<int>("StockID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionID");

                    b.HasIndex("BrokerID");

                    b.HasIndex("StockID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("LondonStockExchange.API.Models.Transaction", b =>
                {
                    b.HasOne("LondonStockExchange.API.Models.Broker", "Broker")
                        .WithMany("Transactions")
                        .HasForeignKey("BrokerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LondonStockExchange.API.Models.Stock", "Stock")
                        .WithMany("Transactions")
                        .HasForeignKey("StockID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Broker");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("LondonStockExchange.API.Models.Broker", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("LondonStockExchange.API.Models.Stock", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}