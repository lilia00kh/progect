using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EntitiesDTO;
using BLL.Interfaces;
using BLL.JwtFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using BLL.Infrastracture;

namespace PL.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var userName = User.Identity.Name;
                var isAdmin = User.IsInRole("Administrator");
                var orders = await _orderService.GetAllOrdersAsynk(isAdmin, userName);
                return Ok(await MapListOfOrdersResponseModels(orders));
            }
            catch (CustomException ex)
            {
                return StatusCode(200, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("changePaymentStatus")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ChangePaymentStatus([FromBody] OrderResponseModel OrderResponseModel)
        {
            try
            {
                var orderId = OrderResponseModel.Id;
                await _orderService.ChangePaymentStatusAsync(orderId);
                return Ok();
            }
            catch (CustomException ex)
            {
                return StatusCode(200, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("deleteOrder")]

        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                await _orderService.DeleteOrderById(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel orderModel)
        {
            try
            {
                if (orderModel == null) { return BadRequest(); }
                var user = User.Identity.Name;
                foreach (var tree in orderModel.Trees)
                {
                   var orderDto = CreateOrderDto(orderModel, user);
                    orderDto.GoodId = tree.TreeId;
                    var deliveryModel = orderModel.Deliveries.FirstOrDefault(x => x.GoodId == tree.TreeId);
                    var paymentModel = orderModel.Payments.FirstOrDefault(x => x.GoodId == tree.TreeId);
                    var detailsAboutTreeId = await _orderService.CreateDetailsAboutGoodAsync(new DetailsAboutGoodDto()
                    {

                        GoodId = tree.TreeId,
                        Price = tree.Price,
                        Size = tree.Size,
                        Count = tree.Count,
                        Name = tree.Name,
                        TypeOfGood = "tree",
                        Color = tree.Color
                    });
                    await CreateOrderAndDetailsAboutIt(orderDto, deliveryModel, paymentModel, detailsAboutTreeId);
                }
                foreach (var toy in orderModel.Toys)
                {
                    var orderDto = CreateOrderDto(orderModel, user);
                    orderDto.GoodId = toy.ToyId;
                    var deliveryModel = orderModel.Deliveries.FirstOrDefault(x => x.GoodId == toy.ToyId);
                    var paymentModel = orderModel.Payments.FirstOrDefault(x => x.GoodId == toy.ToyId);
                    var detailsAboutToyId = await _orderService.CreateDetailsAboutGoodAsync(new DetailsAboutGoodDto()
                    {
                        GoodId = toy.ToyId,
                        Price = toy.Price,
                        Count = toy.Count,
                        Name = toy.Name,
                        TypeOfGood = "toy",
                        Color = ""
                    });
                    await CreateOrderAndDetailsAboutIt(orderDto, deliveryModel, paymentModel, detailsAboutToyId);
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task CreateOrderAndDetailsAboutIt(OrderDto orderDto, DeliveryModel deliveryModel, PaymentModel paymentModel, Guid detailsAboutGoodId)
        {
            Guid orderId = await _orderService.CreateOrderAsynk(orderDto);
            var detailsAboutDeliveryDto = new DetailsAboutDeliveryDto()
            {
                Details = deliveryModel.Details
            };
            Guid detailsAboutDeliveryId=await _orderService.CreateDetailsAboutDeliveryAsynk(detailsAboutDeliveryDto);
            var deliveryDto = new DeliveryDto()
            {
                Name = deliveryModel.Name,
                DetailsAboutDeliveryDtoId = detailsAboutDeliveryId
            };
            Guid deliveryId = await _orderService.CreateDeliveryAsynk(deliveryDto);
            var paymentDto = new PaymentDto()
            {
                Status = paymentModel.Status
            };
            Guid paymentId = await _orderService.CreatePaymentAsynk(paymentDto);

            var orderDeliveryDetailsAboutGoodPaymentDto = new OrderDeliveryDetailsAboutGoodPaymentDto()
            {
                OrderDtoId = orderId,
                DeliveryDtoId = deliveryId,
                DetailsAboutGoodDtoId = detailsAboutGoodId,
                PaymentDtoId = paymentId
            };
            var detailsAboutGoodDto = await _orderService.GetDetailsAboutGoodDtoByIdAsynk(detailsAboutGoodId);
            string detailsAboutGood;
            if (detailsAboutGoodDto.TypeOfGood == "toy") {
                detailsAboutGood = "назва: " + detailsAboutGoodDto.Name
                    + ", кількість: " + detailsAboutGoodDto.Count
                    + ", загальна ціна: " + detailsAboutGoodDto.Price;
            }
            else
            {
                detailsAboutGood = "назва: " + detailsAboutGoodDto.Name
                    + ", розмір: " + detailsAboutGoodDto.Size
                    +", колір: " + detailsAboutGoodDto.Color
                    + ", кількість: " + detailsAboutGoodDto.Count
                    + ", загальна ціна: " + detailsAboutGoodDto.Price;
            }
            await _orderService.CreateOrderDeliveryDetailsAboutGoodPaymentAsynk(orderDeliveryDetailsAboutGoodPaymentDto);
            await _orderService.SendEmailAsync("Замовлення", orderDto.UserEmail, orderDto, deliveryDto.Name, detailsAboutDeliveryDto.Details, paymentDto.Status, detailsAboutGood);
        }

        private OrderDto CreateOrderDto(OrderModel orderModel, string user)
        {
            return new OrderDto()
            {
                User = user,
                FirstName = orderModel.FirstName,
                LastName = orderModel.LastName,
                UserEmail = orderModel.UserEmail,
                Address = orderModel.Address,
                Phone = orderModel.Phone,
                Date = orderModel.Date
            };
            
        }


        private async Task<IEnumerable<OrderResponseModel>> MapListOfOrdersResponseModels(IEnumerable<OrderDto> orderDtos)
        {
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDto, OrderResponseModel>()
                .ForMember(u => u.Id, o => o.MapFrom(x => x.Id))
                .ForMember(u => u.UserEmail, o => o.MapFrom(x => x.UserEmail))
                .ForMember(u => u.User, o => o.MapFrom(x => x.User))
                .ForMember(u => u.FirstName, o => o.MapFrom(x => x.FirstName))
                .ForMember(u => u.LastName, o => o.MapFrom(x => x.LastName))
                .ForMember(u => u.Phone, o => o.MapFrom(x => x.Phone))
                .ForMember(u => u.Address, o => o.MapFrom(x => x.Address))
                 .ReverseMap()
             ).CreateMapper();
            var orderResponseModels = new List<OrderResponseModel>();
            foreach (var order in orderDtos)
            {
                var orderDeliveryDetailsAboutGoodPaymentDto = await _orderService.GetOrderDeliveryDetailsAboutGoodPaymentDtoByOrderIdAsynk(order.Id);
                var deliveryDto = await _orderService.GetDeliveryDtoByIdAsynk(orderDeliveryDetailsAboutGoodPaymentDto.DeliveryDtoId);
                var detailsAboutDelivery = await _orderService.GetDetailsAboutDeliveryDtoByIdAsynk(deliveryDto.DetailsAboutDeliveryDtoId);
                var deliveryDetails = "";
                if (deliveryDto.Name== "самовивіз з магазину") {
                    deliveryDetails = " тип доставки: " + deliveryDto.Name ;
                }
                else
                {
                    deliveryDetails = " тип доставки: " + deliveryDto.Name + ", деталі: " + detailsAboutDelivery.Details;

                }
                var paymentDto = await _orderService.GetPaymentDtoByIdAsynk(orderDeliveryDetailsAboutGoodPaymentDto.PaymentDtoId);
                var paymentDetails = " статус оплати: " + paymentDto.Status;
                var detailsAboutGoodDto = await _orderService.GetDetailsAboutGoodDtoByIdAsynk(orderDeliveryDetailsAboutGoodPaymentDto.DetailsAboutGoodDtoId);
                string goodDetails = "";
                if (detailsAboutGoodDto.TypeOfGood == "tree")
                {
                    goodDetails = " назва товару: " + detailsAboutGoodDto.Name + ", кількість: " + detailsAboutGoodDto.Count + ", ціна: " + detailsAboutGoodDto.Price + ", розмір: " + detailsAboutGoodDto.Size +", колір: " + detailsAboutGoodDto.Color;

                }
                else
                {
                    goodDetails = " назва товару: " + detailsAboutGoodDto.Name + ", кількість: " + detailsAboutGoodDto.Count + ", ціна: " + detailsAboutGoodDto.Price;

                }
                var orderResponse = _mapper.Map<OrderResponseModel>(order);
                orderResponse.DeliveryDetails = deliveryDetails;
                orderResponse.GoodDetails = goodDetails;
                orderResponse.PaymentDetails = paymentDetails;
                orderResponseModels.Add(orderResponse);
            }
            return orderResponseModels;
        }

    }
}
