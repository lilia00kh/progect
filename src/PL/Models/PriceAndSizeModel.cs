using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class PriceAndSizeModel
    {
        [Column("PriceAndSizeModelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Price is a required field.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Size is a required field.")]
        public double Size { get; set; }

    }
}
