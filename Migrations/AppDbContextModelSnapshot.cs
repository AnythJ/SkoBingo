﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkoBingo.Models;

#nullable disable

namespace SkoBingo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SkoBingo.Models.Bingo", b =>
                {
                    b.Property<int>("BingoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("UniqueLink")
                        .HasColumnType("longtext");

                    b.HasKey("BingoId");

                    b.ToTable("Bingos");
                });

            modelBuilder.Entity("SkoBingo.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("ScoreboardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WinDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ScoreboardId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SkoBingo.Models.Scoreboard", b =>
                {
                    b.Property<int>("ScoreboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BingoId")
                        .HasColumnType("int");

                    b.HasKey("ScoreboardId");

                    b.HasIndex("BingoId")
                        .IsUnique();

                    b.ToTable("Scoreboards");
                });

            modelBuilder.Entity("SkoBingo.Models.Sentence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BingoId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BingoId");

                    b.ToTable("Sentences");
                });

            modelBuilder.Entity("SkoBingo.Models.Player", b =>
                {
                    b.HasOne("SkoBingo.Models.Scoreboard", "Scoreboard")
                        .WithMany("Players")
                        .HasForeignKey("ScoreboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scoreboard");
                });

            modelBuilder.Entity("SkoBingo.Models.Scoreboard", b =>
                {
                    b.HasOne("SkoBingo.Models.Bingo", "Bingo")
                        .WithOne("Scoreboard")
                        .HasForeignKey("SkoBingo.Models.Scoreboard", "BingoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bingo");
                });

            modelBuilder.Entity("SkoBingo.Models.Sentence", b =>
                {
                    b.HasOne("SkoBingo.Models.Bingo", "Bingo")
                        .WithMany("Sentence")
                        .HasForeignKey("BingoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bingo");
                });

            modelBuilder.Entity("SkoBingo.Models.Bingo", b =>
                {
                    b.Navigation("Scoreboard");

                    b.Navigation("Sentence");
                });

            modelBuilder.Entity("SkoBingo.Models.Scoreboard", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
