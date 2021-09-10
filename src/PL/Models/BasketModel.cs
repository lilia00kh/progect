using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class BasketModel
    {
        [Column("BasketId")]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "User id is a required field.")]
        public string UserName { get; set; }

        public List<TreeForBasketModel> Trees { get; set; }
        public List<ToyForBasketModel> Toys { get; set; }
        //public List<Guid> GoodIds { get; set; }
    }
}
