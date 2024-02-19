using LondonStockExchange.API.Data;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Repositories
{
    /// <summary>
    /// Repository for managing broker data in the database.
    /// Implements the <see cref="IBrokerRepository"/> interface.
    /// </summary>
    public class BrokerRepository : IBrokerRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context used for data operations.</param>
        public BrokerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all brokers from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Broker"/>.</returns>
        public async Task<IEnumerable<Broker>> GetAllAsync()
        {
            return await _context.Brokers.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a broker by its identifier.
        /// </summary>
        /// <param name="brokerId">The identifier of the broker to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the <see cref="Broker"/> if found; otherwise, null.</returns>
        public async Task<Broker> GetByIdAsync(int brokerId)
        {
            return await _context.Brokers.FindAsync(brokerId);
        }

        /// <summary>
        /// Asynchronously adds a new broker to the database.
        /// </summary>
        /// <param name="broker">The broker to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the added <see cref="Broker"/>.</returns>
        public async Task<Broker> AddAsync(Broker broker)
        {
            _context.Brokers.Add(broker);
            await _context.SaveChangesAsync();
            return broker;
        }

        /// <summary>
        /// Asynchronously updates an existing broker in the database.
        /// </summary>
        /// <param name="broker">The broker with updated information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is the updated <see cref="Broker"/>.</returns>
        public async Task<Broker> UpdateAsync(Broker broker)
        {
            _context.Entry(broker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return broker;
        }

        /// <summary>
        /// Asynchronously deletes a broker from the database by its identifier.
        /// </summary>
        /// <param name="brokerId">The identifier of the broker to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the broker was deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int brokerId)
        {
            var broker = await _context.Brokers.FindAsync(brokerId);
            if (broker == null) return false;

            _context.Brokers.Remove(broker);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}