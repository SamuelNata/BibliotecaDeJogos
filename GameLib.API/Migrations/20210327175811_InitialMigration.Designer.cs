﻿// <auto-generated />
using System;
using GameLib.Repository.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GameLib.API.Migrations
{
    [DbContext(typeof(GameLibDbContext))]
    [Migration("20210327175811_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GameLib.Model.Entity.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.Property<short?>("Year")
                        .HasColumnName("year")
                        .HasColumnType("smallint");

                    b.HasKey("Id")
                        .HasName("pk_game");

                    b.ToTable("game");
                });

            modelBuilder.Entity("GameLib.Model.Entity.GameBorrowing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameBorrowerId")
                        .HasColumnName("game_borrower_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameOwnershipId")
                        .HasColumnName("game_ownership_id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("PredictedEndDate")
                        .HasColumnName("predicted_end_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("RealEndDate")
                        .HasColumnName("real_end_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnName("start_date")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id")
                        .HasName("pk_game_borrowing");

                    b.HasIndex("GameBorrowerId")
                        .HasName("ix_game_borrowing_game_borrower_id");

                    b.HasIndex("GameOwnershipId")
                        .HasName("ix_game_borrowing_game_ownership_id");

                    b.ToTable("game_borrowing");
                });

            modelBuilder.Entity("GameLib.Model.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnName("nickname")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user");
                });

            modelBuilder.Entity("GameLib.Model.Entity.UserGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GameId")
                        .HasColumnName("game_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_user_game");

                    b.HasIndex("GameId")
                        .HasName("ix_user_game_game_id");

                    b.HasIndex("UserId")
                        .HasName("ix_user_game_user_id");

                    b.ToTable("user_game");
                });

            modelBuilder.Entity("GameLib.Model.Entity.GameBorrowing", b =>
                {
                    b.HasOne("GameLib.Model.Entity.User", "GameBorrower")
                        .WithMany()
                        .HasForeignKey("GameBorrowerId")
                        .HasConstraintName("fk_game_borrowing_user_game_borrower_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLib.Model.Entity.UserGame", "GameOwnership")
                        .WithMany("GameBorrowings")
                        .HasForeignKey("GameOwnershipId")
                        .HasConstraintName("fk_game_borrowing_user_game_game_ownership_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameLib.Model.Entity.UserGame", b =>
                {
                    b.HasOne("GameLib.Model.Entity.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("fk_user_game_game_game_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLib.Model.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_user_game_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
