using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EntitiesDTO
{
    public class RecomendationDto
    {
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }
        public string GoodType { get; set; }
    }
}
