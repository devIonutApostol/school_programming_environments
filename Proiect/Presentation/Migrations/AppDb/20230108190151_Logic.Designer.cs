﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Presentation.Data;

#nullable disable

namespace Presentation.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230108190151_Logic")]
    partial class Logic
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("LineItemTargetingRule", b =>
                {
                    b.Property<Guid>("LineItemsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TargetingRulesId")
                        .HasColumnType("TEXT");

                    b.HasKey("LineItemsId", "TargetingRulesId");

                    b.HasIndex("TargetingRulesId");

                    b.ToTable("LineItemTargetingRule");
                });

            modelBuilder.Entity("Presentation.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Presentation.Models.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ContractValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PublisherId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Presentation.Models.Creative", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Creatives");
                });

            modelBuilder.Entity("Presentation.Models.LineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ContractId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Cpm")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreativeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ContractId");

                    b.HasIndex("CreativeId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("Presentation.Models.Publisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Presentation.Models.TargetingRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TargetingRules");
                });

            modelBuilder.Entity("LineItemTargetingRule", b =>
                {
                    b.HasOne("Presentation.Models.LineItem", null)
                        .WithMany()
                        .HasForeignKey("LineItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.TargetingRule", null)
                        .WithMany()
                        .HasForeignKey("TargetingRulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Presentation.Models.Contract", b =>
                {
                    b.HasOne("Presentation.Models.Account", "Account")
                        .WithMany("Contracts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Publisher", "Publisher")
                        .WithMany("Contracts")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("Presentation.Models.Creative", b =>
                {
                    b.HasOne("Presentation.Models.Account", "Account")
                        .WithMany("Creatives")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Presentation.Models.LineItem", b =>
                {
                    b.HasOne("Presentation.Models.Account", "Account")
                        .WithMany("LineItems")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Contract", "Contract")
                        .WithMany("LineItems")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Creative", "Creative")
                        .WithMany()
                        .HasForeignKey("CreativeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Contract");

                    b.Navigation("Creative");
                });

            modelBuilder.Entity("Presentation.Models.TargetingRule", b =>
                {
                    b.HasOne("Presentation.Models.Account", "Account")
                        .WithMany("TargetingRules")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Presentation.Models.Account", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Creatives");

                    b.Navigation("LineItems");

                    b.Navigation("TargetingRules");
                });

            modelBuilder.Entity("Presentation.Models.Contract", b =>
                {
                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("Presentation.Models.Publisher", b =>
                {
                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
