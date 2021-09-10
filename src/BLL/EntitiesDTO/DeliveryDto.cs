using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EntitiesDTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DetailsAboutDeliveryDtoId { get; set; }
    }
}
