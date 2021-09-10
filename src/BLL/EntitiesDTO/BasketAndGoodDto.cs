using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class BasketAndGoodDto
    {
        [Column("BasketAndGoodDtoId")]
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Guid GoodId { get; set; }
        public Guid DetailsAboutGoodId { get; set; }
    }
}
