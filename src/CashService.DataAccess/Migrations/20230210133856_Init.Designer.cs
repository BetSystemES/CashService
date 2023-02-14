﻿// <auto-generated />
using System;
using CashService.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashService.DataAccess.Migrations
{
    [DbContext(typeof(CashDbContext))]
    [Migration("20230210133856_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CashService.BusinessLogic.Models.TransactionEntity", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("CashType")
                        .HasColumnType("integer");

                    b.Property<Guid>("TransactionProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("TransactionId");

                    b.HasIndex("TransactionProfileId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("CashService.BusinessLogic.Models.TransactionProfileEntity", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("ProfileId");

                    b.ToTable("TransactionProfile", (string)null);
                });

            modelBuilder.Entity("CashService.BusinessLogic.Models.TransactionEntity", b =>
                {
                    b.HasOne("CashService.BusinessLogic.Models.TransactionProfileEntity", "TransactionProfileEntity")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionProfileEntity");
                });

            modelBuilder.Entity("CashService.BusinessLogic.Models.TransactionProfileEntity", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}