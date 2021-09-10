using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class TreeForBasketModel
    {
        [Column("TreeForBasketModelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tree id is required field.")]
        public Guid TreeId { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required field.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Size is required fields.")]
        public double Size { get; set; }

        [Required(ErrorMessage = "Count is required fields.")]
        public int Count { get; set; }
        public List<ImageModel> ImageModels { get; set; }

        [Required(ErrorMessage = "Color is required fields.")]
        public string Color { get; set; }
    }
}
