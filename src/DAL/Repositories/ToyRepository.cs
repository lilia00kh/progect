using DAL.Entities;
using DAL.EF;
using DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ToyRepository : RepositoryBase<Toy>, IToyRepository
    {
        public ToyRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task CreateToyAsync(Toy toy)
        {
            await Create(toy);
        }

        public async Task DeleteToyAsync(Toy toy)
        {
            await Delete(toy);
        }

        public async Task<Toy> FindByIdAsync(Guid id)
        {
            var toys = await FindByCondition(x => x.Id == id, trackChanges: false);
            return toys.FirstOrDefault();
        }

        public async Task<IEnumerable<Toy>> GetAllToysAsync()
        {
            var toys = await FindAll(trackChanges: false);
            return toys;
        }

        public async Task UpdateToyAsync(Toy toy)
        {
            await Update(toy);
        }
    }
}
