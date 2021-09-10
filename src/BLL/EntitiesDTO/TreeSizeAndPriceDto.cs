using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class TreeSizeAndPriceDto
    {
        [Column("TreeSizeAndPriceDtoId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TreeId is a required field.")]
        public Guid TreeDtoId { get; set; }
        public virtual TreeDto Tree { get; set; }

        [Required(ErrorMessage = "SizeId is a required field.")]
        public Guid SizeDtoId { get; set; }
        public virtual SizeDto Size { get; set; }

        public decimal Price { get; set; }
    }
}
