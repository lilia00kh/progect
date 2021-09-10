using System;

namespace DAL.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
