using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Recomendation
    {
        [Column("RecomendationId")]
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }
        public string GoodType { get; set; }
    }
}
