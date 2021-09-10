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
        //public List<Guid> GoodIds { get; set; }
        //public List<TreeDto> Trees { get; set; }
        //public List<ToyDto> Toys { get; set; }

    }
}
