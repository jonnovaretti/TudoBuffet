using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TudoBuffet.Website.Entities;
using System.Linq;
using System;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buffet>().Property(e => e.Category)
                                         .HasConversion(v => v.ToString(), v => (BuffetCategory)Enum.Parse(typeof(BuffetCategory), v)).HasMaxLength(20);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                property.AsProperty().Builder.HasMaxLength(256, ConfigurationSource.Convention);
            }
        }
    }
}
