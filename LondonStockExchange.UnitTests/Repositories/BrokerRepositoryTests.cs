using Xunit;
using Microsoft.EntityFrameworkCore;
using LondonStockExchange.API.Data;
using LondonStockExchange.API.Models;
using LondonStockExchange.API.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace LondonStockExchange.UnitTests.Repositories
{
    /// <summary>
    /// Contains unit tests for the BrokerRepository class to ensure it correctly interacts with the database.
    /// </summary>
    public class BrokerRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public BrokerRepositoryTests()
        {
            // Setup in-memory database for testing to avoid impacting the actual database.
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LondonStockExchangeTestDB")
                .Options;

            // Ensure a fresh database for each test run.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        /// <summary>
        /// Verifies that GetAllAsync correctly retrieves all brokers from the database.
        /// </summary>
        [Fact]
        public async Task GetAllAsync_ReturnsAllBrokers()
        {
            // Arrange: Add test data to the in-memory database.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                context.Brokers.Add(new Broker { Name = "Test Broker 1" });
                context.Brokers.Add(new Broker { Name = "Test Broker 2" });
                await context.SaveChangesAsync();
            }

            // Act: Retrieve all brokers using the repository method.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var brokers = await repository.GetAllAsync();

                // Assert: Ensure the correct number of brokers are returned.
                Assert.Equal(2, brokers.Count());
            }
        }

        /// <summary>
        /// Tests GetByIdAsync to verify it returns the correct broker when it exists.
        /// </summary>
        [Fact]
        public async Task GetByIdAsync_ReturnsBroker_WhenBrokerExists()
        {
            // Arrange: Add a broker to the in-memory database and get its ID.
            int brokerId;
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var broker = context.Brokers.Add(new Broker { Name = "Test Broker" });
                await context.SaveChangesAsync();
                brokerId = broker.Entity.BrokerID;
            }

            // Act: Retrieve the broker by ID using the repository.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var foundBroker = await repository.GetByIdAsync(brokerId);

                // Assert: Verify the retrieved broker matches the one added.
                Assert.NotNull(foundBroker);
                Assert.Equal("Test Broker", foundBroker.Name);
            }
        }

        /// <summary>
        /// Ensures that AddAsync correctly adds a broker to the database.
        /// </summary>
        [Fact]
        public async Task AddAsync_AddsBrokerToDatabase()
        {
            // Arrange: Create a new broker object to add.
            var newBroker = new Broker { Name = "New Broker" };

            // Act: Add the new broker to the database using the repository.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var addedBroker = await repository.AddAsync(newBroker);
                await context.SaveChangesAsync();

                // Assert: Verify the broker was added successfully.
                Assert.Equal(1, context.Brokers.Count());
                Assert.Equal("New Broker", addedBroker.Name);
            }
        }

        /// <summary>
        /// Tests UpdateAsync to ensure it correctly updates a broker's information in the database.
        /// </summary>
        [Fact]
        public async Task UpdateAsync_UpdatesBrokerInDatabase()
        {
            // Arrange: Add a broker and then update its name.
            int brokerId;
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var broker = context.Brokers.Add(new Broker { Name = "Original Broker" });
                await context.SaveChangesAsync();
                brokerId = broker.Entity.BrokerID;
            }

            // Act: Retrieve and update the broker using the repository.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var foundBroker = await repository.GetByIdAsync(brokerId);
                foundBroker.Name = "Updated Broker";
                var updatedBroker = await repository.UpdateAsync(foundBroker);
                await context.SaveChangesAsync();

                // Assert: Verify the broker's name was updated.
                Assert.Equal("Updated Broker", updatedBroker.Name);
            }
        }

        /// <summary>
        /// Confirms that DeleteAsync successfully removes a broker from the database.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_DeletesBrokerFromDatabase()
        {
            // Arrange: Add a broker to be deleted.
            int brokerId;
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var broker = context.Brokers.Add(new Broker { Name = "Broker to Delete" });
                await context.SaveChangesAsync();
                brokerId = broker.Entity.BrokerID;
            }

            // Act: Delete the broker using the repository.
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new BrokerRepository(context);
                var result = await repository.DeleteAsync(brokerId);
                await context.SaveChangesAsync();

                // Assert: Ensure the broker was successfully removed.
                Assert.True(result);
                Assert.Equal(0, context.Brokers.Count());
            }
        }
    }
}