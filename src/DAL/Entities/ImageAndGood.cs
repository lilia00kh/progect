using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class ImageAndGood
    {
        public Guid Id { get; set; }
        public virtual Image Image { get; set; }
        public Guid ImageId { get; set; }
        public Guid GoodId { get; set; }
    }
}
