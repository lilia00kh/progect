using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository: IRepositoryBase<Order>
    {
        Task AddOrderAsynk(Order order);
        Task<Order> FindBookByIdAsynk(Guid Id);
        Task UpdateBookAsynk(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsynk();
    }
}
