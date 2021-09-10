using System;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IToyRepository
    {
        Task<IEnumerable<Toy>> GetAllToysAsync();
        Task CreateToyAsync(Toy toy);
        Task DeleteToyAsync(Toy toy);
        Task<Toy> FindByIdAsync(Guid id);
        Task UpdateToyAsync(Toy toy);
    }
}
