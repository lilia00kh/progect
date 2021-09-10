using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class DetailsAboutGoodDto
    {
        [Column("DetailsAboutGoodDtoId")]
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }
        public string Name { get; set; }
        public string TypeOfGood { get; set; }
        public int Count { get; set; }
        public double Size { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
    }
}
