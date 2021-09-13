using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class ToyForBasketModel
    {
        [Column("ToyForBasketModelId")]
        public Guid Id { get; set; }

        public Guid ToyId { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required field.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Count is required fields.")]
        public int Count { get; set; }
        public List<ImageModel> ImageModels { get; set; }
    }
}
