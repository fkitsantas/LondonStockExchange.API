using LondonStockExchange.API.Data;
using LondonStockExchange.API.Interfaces.Repositories;
using LondonStockExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Repositories
{
    public class BrokerRepository : IBrokerRepository
    {
        private readonly ApplicationDbContext _context;

        public BrokerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Broker>> GetAllAsync()
        {
            return await _context.Brokers.ToListAsync();
        }

        public async Task<Broker> GetByIdAsync(int brokerId)
        {
            return await _context.Brokers.FindAsync(brokerId);
        }

        public async Task<Broker> AddAsync(Broker broker)
        {
            _context.Brokers.Add(broker);
            await _context.SaveChangesAsync();
            return broker;
        }

        public async Task<Broker> UpdateAsync(Broker broker)
        {
            _context.Entry(broker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return broker;
        }

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