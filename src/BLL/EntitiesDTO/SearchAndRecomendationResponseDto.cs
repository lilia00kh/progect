using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.EntitiesDTO
{
    public class SearchAndRecomendationResponseDto
    {
        public List<TreeDto> Trees { get; set; }
        public List<ToyDto> Toys { get; set; }
    }
}
