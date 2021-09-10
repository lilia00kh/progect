using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class DetailsAboutGood
    {
        [Column("DetailsAboutGoodId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TypeOfGood { get; set; }
        public int Count { get; set; }
        public double Size { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Color { get; set; }

    }
}
