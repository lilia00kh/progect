using System;

namespace DAL.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual DetailsAboutDelivery DetailsAboutDelivery { get; set; }
        public Guid DetailsAboutDeliveryId { get; set; }
    }
}
