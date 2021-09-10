using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class SizeDto
    {
        [Column("SizeDtoId")]
        public Guid Id { get; set; }

        public double NameOfSize { get; set; }
    }
}
