using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Basket
    {
        [Column("BasketId")]
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
