using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Size
    {
        [Column("SizeId")]
        public Guid Id { get; set; }

        public double NameOfSize { get; set; }
    }
}
