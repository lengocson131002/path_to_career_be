﻿// <auto-generated />
using System;
using ClientService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientService.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230524084649_UpdateModelV2")]
    partial class UpdateModelV2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
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

                    b.Property<string>("CVStlye")
                        .HasColumnType("text");

                    b.Property<string>("CVTyle")
                        .HasColumnType("text");

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

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("JobLevel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MajorId")
                        .HasColumnType("bigint");

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("SupportCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AcceptedAccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.PostApplication", b =>
                {
                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

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

                    b.Property<decimal>("FeePerCount")
                        .HasColumnType("numeric");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("MethodDescription")
                        .HasColumnType("text");

                    b.Property<long>("SupportCount")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("PostId", "ApplierId");

                    b.HasIndex("ApplierId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("PostApplications");
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

                    b.Navigation("AcceptedAccount");

                    b.Navigation("Account");
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

            modelBuilder.Entity("ClientService.Domain.Entities.Account", b =>
                {
                    b.Navigation("PostApplications");
                });

            modelBuilder.Entity("ClientService.Domain.Entities.Post", b =>
                {
                    b.Navigation("PostApplications");
                });
#pragma warning restore 612, 618
        }
    }
}
