using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Tree
    {
        [Column("TreeId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string TreeType { get; set; }
        public string Color { get; set; }

        //public Guid TreeSizeId { get; set; }
        //public virtual TreeSizeAndPrice TreeSize { get; set; }
    }
}
