using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TudoBuffet.Website.Entities;
using System.Linq;
using System;
using TudoBuffet.Website.ValuesObjects;
using TudoBuffet.Website.Models;

namespace TudoBuffet.Website.Repositories.Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        { }

        public DbSet<Buffet> Buffets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EmailValidation> EmailsValidation { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buffet>().Property(e => e.Category)
                                         .HasConversion(v => v.ToString(), v => (BuffetCategory)Enum.Parse(typeof(BuffetCategory), v)).HasMaxLength(20);

            modelBuilder.Entity<Buffet>().Property(e => e.Environment)
                                         .HasConversion(v => v.ToString(), v => (BuffetEnvironment)Enum.Parse(typeof(BuffetEnvironment), v)).HasMaxLength(20);

            modelBuilder.Entity<Buffet>().Property(e => e.Price)
                                         .HasConversion(v => v.ToString(), v => (RangePrice)Enum.Parse(typeof(RangePrice), v)).HasMaxLength(20);

            modelBuilder.Entity<User>().Property(e => e.Profile)
                                         .HasConversion(v => v.ToString(), v => (Profile)Enum.Parse(typeof(Profile), v)).HasMaxLength(20);

            modelBuilder.Entity<BudgetBuffet>().HasKey(bb => new { bb.BudgetId, bb.BuffetId });

            modelBuilder.Entity<BudgetBuffet>()
            .HasOne(pt => pt.Budget)
            .WithMany(p => p.BudgetBuffets)
            .HasForeignKey(pt => pt.BudgetId);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                property.AsProperty().Builder.HasMaxLength(256, ConfigurationSource.Convention);
            }

            modelBuilder.Entity<Plan>().HasData(new Plan()
            {
                CreateAt = DateTime.Now,
                Description = "O plano ouro favorece o aparecimento em mais vezes nas pesquisas e irá aparecer com mais frequencia no destaques do dia",
                Id = Guid.NewGuid(),
                Image = "img/planouro.jpg",
                IsActive = true,
                Name = "Plano ouro",
                Order = 1,
                Price = 30.00M,
                UpdateAt = null
            });

            modelBuilder.Entity<Plan>().HasData(new Plan()
            {
                CreateAt = DateTime.Now,
                Description = "O plano prata está a frente do plano bronze e também irá aparecer nas pesquisa com uma boa frequencia e também estará presente nos destaques do dia",
                Id = Guid.NewGuid(),
                Image = "img/planprata.jpg",
                IsActive = true,
                Name = "Plano prata",
                Order = 2,
                Price = 20.00M,
                UpdateAt = null
            });

            modelBuilder.Entity<Plan>().HasData(new Plan()
            {
                CreateAt = DateTime.Now,
                Description = "O plano bronze irá aparecer nas pesquisas, mas com menos frequencia na primeira página",
                Id = Guid.NewGuid(),
                Image = "img/planbronze.jpg",
                IsActive = true,
                Name = "Plano bronze",
                Order = 3,
                Price = 10.00M,
                UpdateAt = null
            });


        }

        public DbSet<TudoBuffet.Website.Models.BuffetModel> BuffetModel { get; set; }
    }
}
