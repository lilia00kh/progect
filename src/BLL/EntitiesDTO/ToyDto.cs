using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EntitiesDTO
{
    public class ToyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<ImageDto> ImageDtos { get; set; }
    }
}
