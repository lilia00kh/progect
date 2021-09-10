using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class OrderDeliveryDetailsAboutGoodPayment
    {
        public Guid Id { get; set; }
        public virtual Order Order { get; set; }
        public Guid OrderId { get; set; }
        public virtual Delivery Delivery { get; set; }
        public Guid DeliveryId { get; set; }
        public virtual DetailsAboutGood DetailsAboutGood { get; set; }
        public Guid DetailsAboutGoodId { get; set; }
        public virtual Payment Payment { get; set; }
        public Guid PaymentId { get; set; }

    } 
}
