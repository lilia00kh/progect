using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class BasketAndGood
    {
        [Column("BasketAndGoodId")]
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public virtual Basket Basket { get; set; }
        public Guid GoodId { get; set; }
        public virtual DetailsAboutGood DetailsAboutGood { get; set; }
        public Guid DetailsAboutGoodId { get; set; }
    }
}
