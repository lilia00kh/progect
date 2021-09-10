using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EntitiesDTO
{
    public class OrderDeliveryDetailsAboutGoodPaymentDto
    {
        public Guid Id { get; set; }
        public Guid OrderDtoId { get; set; }
        public Guid DeliveryDtoId { get; set; }
        public Guid DetailsAboutGoodDtoId { get; set; }
        public Guid PaymentDtoId { get; set; }
    }
}
