﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TudoBuffet.Website.Repositories.Context;

namespace TudoBuffet.Website.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20190109000411_new")]
    partial class @new
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TudoBuffet.Website.Entities.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<DateTime>("PartyDay");

                    b.Property<Guid?>("PartyOwnerId");

                    b.Property<int>("QuantityPartyGuests");

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.HasIndex("PartyOwnerId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.BudgetDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AnsweredAt");

                    b.Property<Guid?>("BudgetId");

                    b.Property<Guid?>("BuffetId");

                    b.Property<DateTime>("CreateAt");

                    b.Property<bool>("IsDateAvaliable");

                    b.Property<DateTime?>("ProposedDateFor");

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("BuffetId");

                    b.ToTable("BudgetDetail");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.BudgetQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer")
                        .HasMaxLength(256);

                    b.Property<Guid?>("BudgetDetailId");

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Question")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.HasIndex("BudgetDetailId");

                    b.ToTable("BudgetQuestion");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.Buffet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActivedAt");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Cellphone")
                        .HasMaxLength(256);

                    b.Property<string>("City")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("District")
                        .HasMaxLength(256);

                    b.Property<string>("Environment")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Facebook")
                        .HasMaxLength(256);

                    b.Property<string>("Instagram")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("Number")
                        .HasMaxLength(256);

                    b.Property<Guid?>("OwnerId");

                    b.Property<Guid?>("PlanSelectedId");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("State")
                        .HasMaxLength(256);

                    b.Property<string>("Street")
                        .HasMaxLength(256);

                    b.Property<string>("Title")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.Property<string>("Zipcode")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PlanSelectedId");

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

            modelBuilder.Entity("TudoBuffet.Website.Entities.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BuffetId");

                    b.Property<string>("ContainerName")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("DetailFileName")
                        .HasMaxLength(256);

                    b.Property<string>("DetailUrl")
                        .HasMaxLength(256);

                    b.Property<string>("SearchFileName")
                        .HasMaxLength(256);

                    b.Property<string>("SearchUrl")
                        .HasMaxLength(256);

                    b.Property<long>("Size");

                    b.Property<string>("ThumbnailFileName")
                        .HasMaxLength(256);

                    b.Property<string>("ThumbnailUrl")
                        .HasMaxLength(256);

                    b.Property<string>("Type")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.HasIndex("BuffetId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("Image")
                        .HasMaxLength(256);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<int>("Order");

                    b.Property<decimal>("Price");

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.ToTable("Plans");

                    b.HasData(
                        new { Id = new Guid("50083a48-6734-4605-a816-862d6cacb68f"), CreateAt = new DateTime(2019, 1, 8, 22, 4, 9, 609, DateTimeKind.Local), Description = "O plano ouro favorece o aparecimento em mais vezes nas pesquisas e irá aparecer com mais frequencia no destaques do dia", Image = "img/planouro.jpg", IsActive = true, Name = "Plano ouro", Order = 1, Price = 30.00m },
                        new { Id = new Guid("3cbea676-a2cb-4d9e-8612-b37386ffdddf"), CreateAt = new DateTime(2019, 1, 8, 22, 4, 9, 698, DateTimeKind.Local), Description = "O plano prata está a frente do plano bronze e também irá aparecer nas pesquisa com uma boa frequencia e também estará presente nos destaques do dia", Image = "img/planprata.jpg", IsActive = true, Name = "Plano prata", Order = 2, Price = 20.00m },
                        new { Id = new Guid("d2421512-41ff-4f6c-9df8-a0607eec5c1b"), CreateAt = new DateTime(2019, 1, 8, 22, 4, 9, 698, DateTimeKind.Local), Description = "O plano bronze irá aparecer nas pesquisas, mas com menos frequencia na primeira página", Image = "img/planbronze.jpg", IsActive = true, Name = "Plano bronze", Order = 3, Price = 10.00m }
                    );
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActivedAt");

                    b.Property<DateTime>("CreateAt");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(256);

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Salt")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("UpdateAt");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.UserBuffetAdmin", b =>
                {
                    b.HasBaseType("TudoBuffet.Website.Entities.User");


                    b.ToTable("UserBuffetAdmin");

                    b.HasDiscriminator().HasValue("UserBuffetAdmin");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.UserPartyOwner", b =>
                {
                    b.HasBaseType("TudoBuffet.Website.Entities.User");


                    b.ToTable("UserPartyOwner");

                    b.HasDiscriminator().HasValue("UserPartyOwner");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.Budget", b =>
                {
                    b.HasOne("TudoBuffet.Website.Entities.UserPartyOwner", "PartyOwner")
                        .WithMany("Budgets")
                        .HasForeignKey("PartyOwnerId");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.BudgetDetail", b =>
                {
                    b.HasOne("TudoBuffet.Website.Entities.Budget")
                        .WithMany("Details")
                        .HasForeignKey("BudgetId");

                    b.HasOne("TudoBuffet.Website.Entities.Buffet", "Buffet")
                        .WithMany()
                        .HasForeignKey("BuffetId");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.BudgetQuestion", b =>
                {
                    b.HasOne("TudoBuffet.Website.Entities.BudgetDetail")
                        .WithMany("Questions")
                        .HasForeignKey("BudgetDetailId");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.Buffet", b =>
                {
                    b.HasOne("TudoBuffet.Website.Entities.UserBuffetAdmin", "Owner")
                        .WithMany("Buffets")
                        .HasForeignKey("OwnerId");

                    b.HasOne("TudoBuffet.Website.Entities.Plan", "PlanSelected")
                        .WithMany()
                        .HasForeignKey("PlanSelectedId");
                });

            modelBuilder.Entity("TudoBuffet.Website.Entities.Photo", b =>
                {
                    b.HasOne("TudoBuffet.Website.Entities.Buffet", "Buffet")
                        .WithMany("Photos")
                        .HasForeignKey("BuffetId");
                });
#pragma warning restore 612, 618
        }
    }
}
