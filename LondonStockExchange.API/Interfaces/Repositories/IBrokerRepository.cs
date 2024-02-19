using LondonStockExchange.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LondonStockExchange.API.Interfaces.Repositories
{
    /// <summary>
    /// Defines a contract for operations on the Broker entities within the data store.
    /// This interface abstracts the CRUD operations for brokers, facilitating separation of concerns
    /// and promoting a more testable, maintainable architecture.
    /// </summary>
    public interface IBrokerRepository
    {
        /// <summary>
        /// Retrieves all brokers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all <see cref="Broker"/> entities.</returns>
        Task<IEnumerable<Broker>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific broker by their unique identifier asynchronously.
        /// </summary>
        /// <param name="brokerId">The unique identifier of the broker to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Broker"/> entity if found; otherwise, null.</returns>
        Task<Broker> GetByIdAsync(int brokerId);

        /// <summary>
        /// Adds a new broker entity to the data store asynchronously.
        /// </summary>
        /// <param name="broker">The <see cref="Broker"/> entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added <see cref="Broker"/> entity.</returns>
        Task<Broker> AddAsync(Broker broker);

        /// <summary>
        /// Updates an existing broker entity in the data store asynchronously.
        /// </summary>
        /// <param name="broker">The <see cref="Broker"/> entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated <see cref="Broker"/> entity.</returns>
        Task<Broker> UpdateAsync(Broker broker);

        /// <summary>
        /// Deletes a broker entity identified by its unique identifier from the data store asynchronously.
        /// </summary>
        /// <param name="brokerId">The unique identifier of the broker to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the broker was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteAsync(int brokerId);
    }
}