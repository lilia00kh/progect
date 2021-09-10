using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using BLL.JwtFeatures;
using DAL.Entities;
using DAL.Interfaces;
using System;
using BLL.Infrastracture;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BLL.Services.EmailService;
using Microsoft.AspNetCore.WebUtilities;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        private readonly IEmailSender _emailSender;
        public OrderService(IMapper mapper, JwtHandler jwtHandler, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _database = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task<Guid> CreateOrderAsynk(OrderDto orderDto)
        {
            var order = _mapper.Map<OrderDto, Order>(orderDto);
            order.Status = "new";
            await _database.Order.Create(order);
            _database.Save();
            return order.Id;
        }

        public async Task<Guid> CreatePaymentAsynk(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<PaymentDto, Payment>(paymentDto);
            payment.Status = "не оплачено";
            await _database.Payment.Create(payment);
            _database.Save();
            return payment.Id;
        }

        public async Task<Guid> CreateDeliveryAsynk(DeliveryDto deliveryDto)
        {
            var delivery = _mapper.Map<DeliveryDto, Delivery>(deliveryDto);
            await _database.Delivery.Create(delivery);
            _database.Save();
            return delivery.Id;
        }

        public async Task<Guid> CreateDetailsAboutDeliveryAsynk(DetailsAboutDeliveryDto detailsAboutDeliveryDto)
        {
            var detailsAboutDelivery = _mapper.Map<DetailsAboutDeliveryDto, DetailsAboutDelivery>(detailsAboutDeliveryDto);
            await _database.DetailsAboutDelivery.Create(detailsAboutDelivery);
            _database.Save();
            return detailsAboutDelivery.Id;
        }

        public async Task<Guid> CreateOrderDeliveryDetailsAboutGoodPaymentAsynk(OrderDeliveryDetailsAboutGoodPaymentDto orderDeliveryDetailsAboutGoodPaymentDto)
        {
            var orderDeliveryDetailsAboutGoodPayment = _mapper.Map<OrderDeliveryDetailsAboutGoodPaymentDto, OrderDeliveryDetailsAboutGoodPayment>(orderDeliveryDetailsAboutGoodPaymentDto);
            await _database.OrderDeliveryDetailsAboutGoodPayment.Create(orderDeliveryDetailsAboutGoodPayment);
            _database.Save();
            return orderDeliveryDetailsAboutGoodPayment.Id;
        }

        public async Task<Guid> CreateDetailsAboutGoodAsync(DetailsAboutGoodDto detailsAboutGoodDto)
        {
            var details = new DetailsAboutGood()
            {
                Color = detailsAboutGoodDto.Color,
                Name = detailsAboutGoodDto.Name,
                TypeOfGood = detailsAboutGoodDto.TypeOfGood,
                Count = detailsAboutGoodDto.Count,
                Size = detailsAboutGoodDto.Size,
                Price = detailsAboutGoodDto.Price
            };
            await _database.DetailsAboutGood.Create(details);
            _database.Save();
            return details.Id;
        }
        public async Task SendEmailAsync(string mess, string userEmail, OrderDto orderDto, string deliveryDtoName, string detailsAboutDeliveryDtoDetails, string paymentDtoStatus, string detailsAboutGood)
        {
            string callback;
            if (deliveryDtoName== "самовивіз з магазину") {
                callback = "Замовлення: "
                    + "<br>Прізвище: " + orderDto.LastName
                    + "<br>Ім'я: " + orderDto.FirstName
                    + "<br>Номер телефону: " + orderDto.Phone
                    + "<br>Пошта: " + orderDto.UserEmail
                    + "<br>Адресa: " + orderDto.Address
                    + "<br>Деталі про продукт: " + detailsAboutGood
                    + "<br>Доставка: " + deliveryDtoName
                    + "<br>Оплата: " + paymentDtoStatus;
            }
            else {
                callback = "Замовлення: "
                    + "<br>Прізвище: " + orderDto.LastName
                    + "<br>Ім'я: " + orderDto.FirstName
                    + "<br>Номер телефону: " + orderDto.Phone
                    + "<br>Пошта: " + orderDto.UserEmail
                    + "<br>Адресa: " + orderDto.Address
                    + "<br>Деталі про продукт: " + detailsAboutGood
                    + "<br>Доставка: " + deliveryDtoName
                    + "<br>Деталі доставки: " + detailsAboutDeliveryDtoDetails
                    + "<br>Оплата: " + paymentDtoStatus;
            }
            
            var message = new Message(new string[] { userEmail }, mess, callback, null);
            await _emailSender.SendEmailAsync(message);
            var adminEmail = _emailSender.GetAdminEmail();
            var messageToAdmin = new Message(new string[] { adminEmail }, mess, callback, null);
            await _emailSender.SendEmailAsync(messageToAdmin);

        }
        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsynk(bool isAdmin, string userName)
        {
            var orders = await _database.Order.GetAllOrdersAsynk();
            var list = orders.OrderByDescending(x => x.Date).ToList();
            List<OrderDto> ordersList = new List<OrderDto>();
            if (isAdmin)
            {
                return Map(list);
            }
            foreach (var order in list)
            {
                if (order.User == userName)
                    ordersList.Add(_mapper.Map<Order, OrderDto>(order));

            }
            if (ordersList.Count == 0)
                throw new CustomException("Your list of orders is empty", "");
            return ordersList;
        }

        private List<OrderDto> Map(IEnumerable<Order> orders)
        {
            List<OrderDto> ordersList = new List<OrderDto>();
            foreach (var order in orders)
            {
                ordersList.Add(_mapper.Map<Order, OrderDto>(order));
            }

            return ordersList;
        }

        public async Task<Guid> GetDetailsAboutGoodId(DetailsAboutGoodDto detailsAboutGoodDto)
        {
            DetailsAboutGoodDto detailsAboutGood;
            if (detailsAboutGoodDto.TypeOfGood == "tree")
            {
                var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Price == detailsAboutGoodDto.Price && x.Size== detailsAboutGoodDto.Size && x.Count == detailsAboutGoodDto.Count, trackChanges: false);
                detailsAboutGood = _mapper.Map<DetailsAboutGood, DetailsAboutGoodDto>(detailsAboutGoodIQueryable.FirstOrDefault());
                
            }
            else
            {
                var detailsAboutGoodIQueryable = await _database.DetailsAboutGood.FindByCondition(x => x.Price == detailsAboutGoodDto.Price && x.Count == detailsAboutGoodDto.Count, trackChanges: false);
                detailsAboutGood = _mapper.Map<DetailsAboutGood, DetailsAboutGoodDto>(detailsAboutGoodIQueryable.FirstOrDefault());
            }

            return detailsAboutGood.Id;


        }

        public async Task<OrderDeliveryDetailsAboutGoodPaymentDto> GetOrderDeliveryDetailsAboutGoodPaymentDtoByOrderIdAsynk(Guid orderId)
        {
            return _mapper.Map<OrderDeliveryDetailsAboutGoodPayment, OrderDeliveryDetailsAboutGoodPaymentDto>((await _database.OrderDeliveryDetailsAboutGoodPayment.FindByCondition(x => x.OrderId == orderId, trackChanges: false)).FirstOrDefault());
        }

        public async Task<DeliveryDto> GetDeliveryDtoByIdAsynk(Guid deliveryId)
        {
            return _mapper.Map<Delivery, DeliveryDto>((await _database.Delivery.FindByCondition(x => x.Id == deliveryId, trackChanges: false)).FirstOrDefault());
        }

        public async Task<DetailsAboutGoodDto> GetDetailsAboutGoodDtoByIdAsynk(Guid detailsAboutGoodId)
        {
            return _mapper.Map<DetailsAboutGood, DetailsAboutGoodDto>((await _database.DetailsAboutGood.FindByCondition(x => x.Id == detailsAboutGoodId, trackChanges: false)).FirstOrDefault());
        }

        public async Task<DetailsAboutDeliveryDto> GetDetailsAboutDeliveryDtoByIdAsynk(Guid detailsAboutDeliveryId)
        {
            return _mapper.Map<DetailsAboutDelivery, DetailsAboutDeliveryDto>((await _database.DetailsAboutDelivery.FindByCondition(x => x.Id == detailsAboutDeliveryId, trackChanges: false)).FirstOrDefault());
        }

        public async Task<PaymentDto> GetPaymentDtoByIdAsynk(Guid paymentId)
        {
            return _mapper.Map<Payment, PaymentDto>((await _database.Payment.FindByCondition(x => x.Id == paymentId, trackChanges: false)).FirstOrDefault());
        }

        public async Task DeleteOrderById(Guid id)
        {
            var order = (await _database.Order.FindByCondition(x => x.Id == id, false)).FirstOrDefault();
            await _database.Order.Delete(order);
            var detailsAboutOrder = (await _database.OrderDeliveryDetailsAboutGoodPayment.FindByCondition(x => x.OrderId == id,false)).FirstOrDefault();
            var payment = (await _database.Payment.FindByCondition(x => x.Id == detailsAboutOrder.PaymentId, false)).FirstOrDefault();
            var delivery = (await _database.Delivery.FindByCondition(x => x.Id == detailsAboutOrder.DeliveryId, false)).FirstOrDefault();
            var deliveryDetails = (await _database.DetailsAboutDelivery.FindByCondition(x => x.Id == delivery.DetailsAboutDeliveryId, false)).FirstOrDefault();
            await _database.Payment.Delete(payment);
            await _database.Delivery.Delete(delivery);
            await _database.OrderDeliveryDetailsAboutGoodPayment.Delete(detailsAboutOrder);
            await _database.DetailsAboutDelivery.Delete(deliveryDetails);
            _database.Save();
        }

        public async Task ChangePaymentStatusAsync(Guid orderId)
        {
            var orderDetails = (await _database.OrderDeliveryDetailsAboutGoodPayment.FindByCondition(x => x.OrderId == orderId, false)).FirstOrDefault();
            var payment = (await _database.Payment.FindByCondition(x => x.Id == orderDetails.PaymentId, false)).FirstOrDefault();
            payment.Status = "оплачено";
            await _database.Payment.Update(payment);
            _database.Save();
        }
    }
}
