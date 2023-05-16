﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stavki.Infrastructure.EF;

#nullable disable

namespace Stavki.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230516042027_Initial13")]
    partial class Initial13
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.CommentDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DataSourceType")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.RequestDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArrivalCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CargoWeight")
                        .HasColumnType("int");

                    b.Property<int>("ContainerSize")
                        .HasColumnType("int");

                    b.Property<string>("DepartureCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("RequestCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Requests", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.Stavki.InCityDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityType")
                        .HasColumnType("int");

                    b.Property<int?>("Distance")
                        .HasColumnType("int");

                    b.Property<int?>("From24UpTo27Tons")
                        .HasColumnType("int");

                    b.Property<int?>("From27Tons")
                        .HasColumnType("int");

                    b.Property<int?>("UpTo24Tons")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InCity", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.Stavki.InCityNDSDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityType")
                        .HasColumnType("int");

                    b.Property<int?>("Distance")
                        .HasColumnType("int");

                    b.Property<int?>("From24UpTo27Tons")
                        .HasColumnType("int");

                    b.Property<int?>("From27Tons")
                        .HasColumnType("int");

                    b.Property<int?>("UpTo24Tons")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InCityNDS", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.Stavki.NearInCityDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityType")
                        .HasColumnType("int");

                    b.Property<int?>("Distance")
                        .HasColumnType("int");

                    b.Property<int?>("Feet20")
                        .HasColumnType("int");

                    b.Property<int?>("Feet40")
                        .HasColumnType("int");

                    b.Property<int?>("From24UpTo30Tons")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NearInCity", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.Stavki.NearInCityNDSDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityType")
                        .HasColumnType("int");

                    b.Property<int?>("Distance")
                        .HasColumnType("int");

                    b.Property<int?>("Feet20")
                        .HasColumnType("int");

                    b.Property<int?>("Feet40")
                        .HasColumnType("int");

                    b.Property<int?>("From24UpTo30Tons")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NearInCityNDS", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.UserDataDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersData", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.UserDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DataSourceType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("INN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KPP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OGRN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OKPO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.CommentDomain", b =>
                {
                    b.HasOne("Stavki.Infrastructure.EF.Domains.RequestDomain", "Request")
                        .WithMany("Comments")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.RequestDomain", b =>
                {
                    b.HasOne("Stavki.Infrastructure.EF.Domains.UserDomain", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.UserDataDomain", b =>
                {
                    b.HasOne("Stavki.Infrastructure.EF.Domains.UserDomain", "User")
                        .WithOne("UserData")
                        .HasForeignKey("Stavki.Infrastructure.EF.Domains.UserDataDomain", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.RequestDomain", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Stavki.Infrastructure.EF.Domains.UserDomain", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("UserData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
