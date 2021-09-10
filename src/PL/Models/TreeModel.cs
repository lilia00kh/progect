using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class TreeModel
    {
        [Column("TreeModelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required field.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Tree type is required field.")]
        public string TreeType { get; set; }

        [Required(ErrorMessage = "Price and size are required fields.")]
        public List<PriceAndSizeModel> PriceAndSizeModels { get; set; }
        [Required(ErrorMessage = "Image is required field.")]
        public List<ImageModel> ImageModels { get; set; }

        [Required(ErrorMessage = "Color is required fields.")]
        public string Color { get; set; }
    }
}
