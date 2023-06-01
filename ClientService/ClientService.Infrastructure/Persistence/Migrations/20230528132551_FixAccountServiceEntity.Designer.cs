﻿// <auto-generated />
using System;
using ClientService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientService.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230528132551_FixAccountServiceEntity")]
    partial class FixAccountServiceEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccountMajor", b =>
                {
                    b.Property<long>("AccountsId")
                        .HasColumnType("bigint");

                    b.Property<long>("MajorsId")
                        .HasColumnType("bigint");

                    b.HasKey("AccountsId", "MajorsId");

                    b.HasIndex("MajorsId");

                    b.ToTable("AccountMajor");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.AccountService", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<int>("ApplyCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("CancelTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<long>("ServiceId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TransactionId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TransactionId");

                    b.ToTable("AccountServices");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Major", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("AcceptedAccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<int?>("CVStyle")
                        .HasColumnType("integer");

                    b.Property<int?>("CVType")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("FinishTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("JobLevel")
                        .HasColumnType("integer");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MajorId")
                        .HasColumnType("bigint");

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ServiceType")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int?>("SupportCount")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AcceptedAccountId");

                    b.HasIndex("AccountId");

                    b.HasIndex("MajorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.PostApplication", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ApplicationStatus")
                        .HasColumnType("integer");

                    b.Property<long>("ApplierId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("ExperienceDescription")
                        .HasColumnType("text");

                    b.Property<decimal?>("FeePerCount")
                        .HasColumnType("numeric");

                    b.Property<string>("MethodDescription")
                        .HasColumnType("text");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SupportCount")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplierId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.ToTable("PostApplications");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<long>("ReviewerId")
                        .HasColumnType("bigint");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Service", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("ApplyCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("OnTop")
                        .HasColumnType("boolean");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<bool>("Recommended")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("PayMethod")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("PaymentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReferenceTransactionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AccountMajor", b =>
                {
                    b.HasOne("ClientService.Domain.Entities.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientService.Domain.Entities.Major", null)
                        .WithMany()
                        .HasForeignKey("MajorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClientService.Domain.Entities.AccountService", b =>
                {
                    b.HasOne("ClientService.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientService.Domain.Entities.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientService.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Service");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Post", b =>
                {
                    b.HasOne("ClientService.Domain.Entities.Account", "AcceptedAccount")
                        .WithMany()
                        .HasForeignKey("AcceptedAccountId");

                    b.HasOne("ClientService.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientService.Domain.Entities.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AcceptedAccount");

                    b.Navigation("Account");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.PostApplication", b =>
                {
                    b.HasOne("ClientService.Domain.Entities.Account", "Applier")
                        .WithMany("PostApplications")
                        .HasForeignKey("ApplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientService.Domain.Entities.Post", "Post")
                        .WithMany("PostApplications")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applier");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Review", b =>
                {
                    b.HasOne("ClientService.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientService.Domain.Entities.Account", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("ClientService.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Account", b =>
                {
                    b.Navigation("PostApplications");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Post", b =>
                {
                    b.Navigation("PostApplications");
                });
#pragma warning restore 612, 618
        }
    }
}