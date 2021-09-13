using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.EntitiesDTO
{
    public class BasketDto
    {
        [Column("BasketId")]
        public Guid Id { get; set; }
        public string UserName { get; set; }

    }
}
