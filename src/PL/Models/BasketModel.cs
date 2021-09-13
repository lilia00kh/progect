using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class BasketModel
    {
        [Column("BasketId")]
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public List<TreeForBasketModel> Trees { get; set; }
        public List<ToyForBasketModel> Toys { get; set; }
    }
}
