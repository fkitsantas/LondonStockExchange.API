using Microsoft.EntityFrameworkCore;
using LondonStockExchange.API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LondonStockExchange.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.TickerSymbol)
                .IsUnique();

            modelBuilder.Entity<Stock>()
                .Property(s => s.CurrentPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Shares)
                .HasColumnType("decimal(18, 4)");

            // Relationships
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Stock)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.StockID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Broker)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BrokerID);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            try
            {
                ValidateEntities();
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            try
            {
                ValidateEntities();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }

        private void ValidateEntities()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.Entity is IValidatableObject && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(entity.Entity);
                if (!Validator.TryValidateObject(entity.Entity, context, validationResults, true))
                {
                    var errorMessages = validationResults.Select(r => r.ErrorMessage).ToArray();
                    throw new ValidationException("Entity validation failed for " + string.Join(", ", errorMessages));
                }
            }
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Transaction && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Transaction)entity.Entity).Timestamp = DateTime.UtcNow;
                }
            }
        }
    }
}