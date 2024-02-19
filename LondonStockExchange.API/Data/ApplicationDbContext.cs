using Microsoft.EntityFrameworkCore;
using LondonStockExchange.API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace LondonStockExchange.API.Data
{
    /// <summary>
    /// Represents the database context for the London Stock Exchange application, 
    /// containing sets of brokers, stocks, and transactions.
    /// This class is responsible for configuring entity relationships, validating entities before saving,
    /// and automatically setting timestamps for transactions.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for brokers.
        /// </summary>
        public DbSet<Broker> Brokers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for stocks.
        /// </summary>
        public DbSet<Stock> Stocks { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for transactions.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Configures the model entities and their relationships.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint on Stock's TickerSymbol to ensure uniqueness across entries.
            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.TickerSymbol)
                .IsUnique();

            // Configuring decimal precision for financial fields.
            modelBuilder.Entity<Stock>()
                .Property(s => s.CurrentPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Shares)
                .HasColumnType("decimal(18, 4)");

            // Defining relationships between entities.
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Stock)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.StockID);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Broker)
                .WithMany(b => b.Transactions)
                .HasForeignKey(t => t.BrokerID);
        }

        /// <summary>
        /// Saves all changes made in this context to the database with entity validation and timestamp updates.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges()
        {
            AddTimestamps();
            ValidateEntities();
            return base.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database with entity validation and timestamp updates.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            ValidateEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Validates entities that implement <see cref="IValidatableObject"/> and are added or modified.
        /// </summary>
        private void ValidateEntities()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.Entity is IValidatableObject && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(entity.Entity);
                Validator.TryValidateObject(entity.Entity, context, validationResults, true);

                if (validationResults.Any())
                {
                    throw new ValidationException("Entity validation failed for " + string.Join(", ", validationResults.Select(r => r.ErrorMessage)));
                }
            }
        }

        /// <summary>
        /// Adds a timestamp to transactions that are newly added to the context.
        /// </summary>
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