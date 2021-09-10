using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsynk()
        {
            var orders = await FindAll(trackChanges: false);
            return orders;
        }
        public async Task AddOrderAsynk(Order order)
        {
            order.Status = "new";
            await Create(order);
        }

        public async Task<Order> FindBookByIdAsynk(Guid Id)
        {
            var orders = await FindByCondition(x => x.Id == Id, trackChanges: false);
            return orders.FirstOrDefault();
        }

        public async Task UpdateBookAsynk(Order order)
        {
            await Update(order);
        }
    }
}