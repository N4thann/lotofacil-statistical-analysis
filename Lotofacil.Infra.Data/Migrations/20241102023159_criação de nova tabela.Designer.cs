﻿// <auto-generated />
using System;
using Lotofacil.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lotofacil.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241102023159_criação de nova tabela")]
    partial class criaçãodenovatabela
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lotofacil.Domain.Entities.BaseContest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2")
                        .HasColumnName("Data");

                    b.Property<int>("Matched11")
                        .HasColumnType("int");

                    b.Property<int>("Matched12")
                        .HasColumnType("int");

                    b.Property<int>("Matched13")
                        .HasColumnType("int");

                    b.Property<int>("Matched14")
                        .HasColumnType("int");

                    b.Property<int>("Matched15")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Name");

                    b.Property<string>("Numbers")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("Numbers");

                    b.HasKey("Id");

                    b.ToTable("BaseContests");
                });

            modelBuilder.Entity("Lotofacil.Domain.Entities.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BaseContestId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2")
                        .HasColumnName("Data");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Name");

                    b.Property<string>("Numbers")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)")
                        .HasColumnName("Numbers");

                    b.HasKey("Id");

                    b.HasIndex("BaseContestId");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("Lotofacil.Domain.Entities.Contest", b =>
                {
                    b.HasOne("Lotofacil.Domain.Entities.BaseContest", null)
                        .WithMany("ContestsAbove11")
                        .HasForeignKey("BaseContestId");
                });

            modelBuilder.Entity("Lotofacil.Domain.Entities.BaseContest", b =>
                {
                    b.Navigation("ContestsAbove11");
                });
#pragma warning restore 612, 618
        }
    }
}
