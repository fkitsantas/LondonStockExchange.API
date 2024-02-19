using Xunit;
using Microsoft.EntityFrameworkCore;
using LondonStockExchange.API.Data;
using LondonStockExchange.API.Models;
using LondonStockExchange.API.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace LondonStockExchange.UnitTests.Repositories
{
    public class BrokerRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public BrokerRepositoryTests()
        {
            // Setup in-memory database option
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LondonStockExchangeTestDB")
                .Options;

            // Initialize the database with data
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllBrokers()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Brokers.Add(new Broker { Name = "Test Broker 1" });
                context.Brokers.Add(new Broker { Name = "Test Broker 2" });
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var brokers = await repository.GetAllAsync();

                // Assert
                Assert.Equal(2, brokers.Count());
            }
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsBroker_WhenBrokerExists()
        {
            // Arrange
            int brokerId;
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var broker = context.Brokers.Add(new Broker { Name = "Test Broker" });
                await context.SaveChangesAsync();
                brokerId = broker.Entity.BrokerID;
            }

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var foundBroker = await repository.GetByIdAsync(brokerId);

                // Assert
                Assert.NotNull(foundBroker);
                Assert.Equal("Test Broker", foundBroker.Name);
            }
        }

        [Fact]
        public async Task AddAsync_AddsBrokerToDatabase()
        {
            // Arrange
            var newBroker = new Broker { Name = "New Broker" };

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var addedBroker = await repository.AddAsync(newBroker);
                await context.SaveChangesAsync();

                // Assert
                Assert.Equal(1, context.Brokers.Count());
                Assert.Equal("New Broker", addedBroker.Name);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesBrokerInDatabase()
        {
            // Arrange
            int brokerId;
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var broker = context.Brokers.Add(new Broker { Name = "Original Broker" });
                await context.SaveChangesAsync();
                brokerId = broker.Entity.BrokerID;
            }

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var foundBroker = await repository.GetByIdAsync(brokerId);
                foundBroker.Name = "Updated Broker";
                var updatedBroker = await repository.UpdateAsync(foundBroker);
                await context.SaveChangesAsync();

                // Assert
                Assert.Equal("Updated Broker", updatedBroker.Name);
            }
        }

        [Fact]
        public async Task DeleteAsync_DeletesBrokerFromDatabase()
        {
            // Arrange
            int brokerId;
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var broker = context.Brokers.Add(new Broker { Name = "Broker to Delete" });
                await context.SaveChangesAsync();
                brokerId = broker.Entity.BrokerID;
            }

            // Act
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var result = await repository.DeleteAsync(brokerId);
                await context.SaveChangesAsync();

                // Assert
                Assert.True(result);
                Assert.Equal(0, context.Brokers.Count());
            }
        }
    }
}