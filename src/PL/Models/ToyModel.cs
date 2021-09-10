using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PL.Models
{
    public class ToyModel
    {
        [Column("ToyModelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required field.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required field.")]
        public double Price { get; set; }
        public List<ImageModel> ImageModels { get; set; }
    }
}
