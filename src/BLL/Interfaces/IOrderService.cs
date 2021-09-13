using BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsynk(OrderDto orderDto);
        Task<Guid> CreatePaymentAsynk(PaymentDto paymentDto);
        Task<Guid> CreateDeliveryAsynk(DeliveryDto deliveryDto);
        Task<Guid> CreateDetailsAboutDeliveryAsynk(DetailsAboutDeliveryDto detailsAboutDeliveryDto);
        Task<Guid> CreateOrderDeliveryDetailsAboutGoodPaymentAsynk(OrderDeliveryDetailsAboutGoodPaymentDto orderDeliveryDetailsAboutGoodPaymentDto);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsynk(bool isAdmin, string userName);
        Task<Guid> CreateDetailsAboutGoodAsync(DetailsAboutGoodDto detailsAboutGoodDto);
        Task<OrderDeliveryDetailsAboutGoodPaymentDto> GetOrderDeliveryDetailsAboutGoodPaymentDtoByOrderIdAsynk(Guid orderId);
        Task<DeliveryDto> GetDeliveryDtoByIdAsynk(Guid deliveryId);
        Task<DetailsAboutGoodDto> GetDetailsAboutGoodDtoByIdAsynk(Guid detailsAboutGoodId);
        Task<DetailsAboutDeliveryDto> GetDetailsAboutDeliveryDtoByIdAsynk(Guid detailsAboutDeliveryId);
        Task<PaymentDto> GetPaymentDtoByIdAsynk(Guid paymentId);
        Task SendEmailAsync(string mess, string userEmail, OrderDto orderDto, string deliveryDtoName, string detailsAboutDeliveryDtoDetails, string paymentDtoStatus, string detailsAboutGood);
        Task DeleteOrderById(Guid id);
        Task ChangePaymentStatusAsync(Guid orderId);
    }
}
