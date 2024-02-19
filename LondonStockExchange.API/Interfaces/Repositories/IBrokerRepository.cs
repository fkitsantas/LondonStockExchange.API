using LondonStockExchange.API.Models;
using LondonStockExchange.API.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Repositories
{
    public interface IBrokerRepository
    {
        Task<IEnumerable<Broker>> GetAllAsync();
        Task<Broker> GetByIdAsync(int brokerId);
        Task<Broker> AddAsync(Broker broker);
        Task<Broker> UpdateAsync(Broker broker);
        Task<bool> DeleteAsync(int brokerId);
    }
}