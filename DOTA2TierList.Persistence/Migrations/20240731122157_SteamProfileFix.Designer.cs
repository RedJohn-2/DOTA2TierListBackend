﻿// <auto-generated />
using System;
using DOTA2TierList.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DOTA2TierList.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240731122157_SteamProfileFix")]
    partial class SteamProfileFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "VerifyUser"
                        },
                        new
                        {
                            Id = 4,
                            Name = "HighMMRUser"
                        });
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.SteamProfileEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("MatchMakingRating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SteamProfiles");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<long>("TierListId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TierListId");

                    b.ToTable("Tiers");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("Id");

                    b.ToTable("TierItems");

                    b.HasDiscriminator().HasValue("TierItemEntity");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierListEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("TierLists");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierListTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.HasKey("Id");

                    b.ToTable("TierListTypeEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "HeroesTierList"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ArtifactTierList"
                        });
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("SteamProfileId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SteamProfileId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleEntityUserEntity", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<long>("UsersId")
                        .HasColumnType("bigint");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleEntityUserEntity");
                });

            modelBuilder.Entity("TierEntityTierItemEntity", b =>
                {
                    b.Property<int>("ItemsId")
                        .HasColumnType("integer");

                    b.Property<long>("TiersId")
                        .HasColumnType("bigint");

                    b.HasKey("ItemsId", "TiersId");

                    b.HasIndex("TiersId");

                    b.ToTable("TierEntityTierItemEntity");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierItemTypes.ArtifactEntity", b =>
                {
                    b.HasBaseType("DOTA2TierList.Persistence.Entities.TierItemEntity");

                    b.HasDiscriminator().HasValue("ArtifactEntity");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierItemTypes.HeroEntity", b =>
                {
                    b.HasBaseType("DOTA2TierList.Persistence.Entities.TierItemEntity");

                    b.HasDiscriminator().HasValue("HeroEntity");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierEntity", b =>
                {
                    b.HasOne("DOTA2TierList.Persistence.Entities.TierListEntity", "TierList")
                        .WithMany("Tiers")
                        .HasForeignKey("TierListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TierList");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierListEntity", b =>
                {
                    b.HasOne("DOTA2TierList.Persistence.Entities.TierListTypeEntity", "Type")
                        .WithMany("TierLists")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOTA2TierList.Persistence.Entities.UserEntity", "User")
                        .WithMany("TierLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.UserEntity", b =>
                {
                    b.HasOne("DOTA2TierList.Persistence.Entities.SteamProfileEntity", "SteamProfile")
                        .WithOne("User")
                        .HasForeignKey("DOTA2TierList.Persistence.Entities.UserEntity", "SteamProfileId");

                    b.Navigation("SteamProfile");
                });

            modelBuilder.Entity("RoleEntityUserEntity", b =>
                {
                    b.HasOne("DOTA2TierList.Persistence.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOTA2TierList.Persistence.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TierEntityTierItemEntity", b =>
                {
                    b.HasOne("DOTA2TierList.Persistence.Entities.TierItemEntity", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DOTA2TierList.Persistence.Entities.TierEntity", null)
                        .WithMany()
                        .HasForeignKey("TiersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.SteamProfileEntity", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierListEntity", b =>
                {
                    b.Navigation("Tiers");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.TierListTypeEntity", b =>
                {
                    b.Navigation("TierLists");
                });

            modelBuilder.Entity("DOTA2TierList.Persistence.Entities.UserEntity", b =>
                {
                    b.Navigation("TierLists");
                });
#pragma warning restore 612, 618
        }
    }
}
