using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class TreeSizeAndPrice
    {
        [Column("TreeSizeAndPriceId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "TreeId is a required field.")]
        public Guid TreeId { get; set; }
        public virtual Tree Tree { get; set; }

        [Required(ErrorMessage = "SizeId is a required field.")]
        public Guid SizeId { get; set; }
        public virtual Size Size { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Price { get; set; }
    }
}