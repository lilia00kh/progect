using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class TreeDto
    {
        [Column("TreeDtoId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Name { get; set; }
        public string TreeType { get; set; }

        public string Description { get; set; }
        public List<TreeSizeAndPriceDto> PriceAndSizeDtos { get; set; }
        public List<ImageDto> ImageDtos { get; set; }
        public string Color { get; set; }
        //public Guid TreeSizeAndPriceDtoId { get; set; }
        //public virtual TreeSizeAndPriceDto TreeSizeAndPriceDto { get; set; }
    }
}
