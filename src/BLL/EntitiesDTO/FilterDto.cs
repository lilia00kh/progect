using System.Collections.Generic;

namespace BLL.EntitiesDTO
{
    public class FilterDto
    {
        public List<string> TreeColors { get; set; }
        public List<double> TreeSizes { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
