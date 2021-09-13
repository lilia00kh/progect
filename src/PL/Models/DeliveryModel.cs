using System;
using System.ComponentModel.DataAnnotations;


namespace PL.Models
{
    public class DeliveryModel
    {
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the name is 60 characters.")]
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
