﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TudoBuffet.Website.Repositories.Context;

namespace TudoBuffet.Website.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TudoBuffet.Website.Entities.Buffet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("CelPhone")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Facebook")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("Thumbprint")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Buffets");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.EmailValidation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<DateTime>("ExpireAt");

                    b.Property<string>("Token")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.Property<DateTime?>("ValidateAt");

                    b.Property<bool>("WasValidate");

                    b.HasKey("Id");

                    b.ToTable("EmailsValidation");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActivedAt");

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(256);

                    b.Property<string>("Salt")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.Buffet", b =>
                {
                    b.HasOne("TudoBuffet.Website.Entities.User")
                        .WithMany("Buffets")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
