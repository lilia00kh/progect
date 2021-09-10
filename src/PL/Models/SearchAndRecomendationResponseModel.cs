using System.Collections.Generic;

namespace PL.Models
{
    public class SearchAndRecomendationResponseModel
    {
        public List<TreeModel> Trees { get; set; }
        public List<ToyModel> Toys { get; set; }
    }
}
