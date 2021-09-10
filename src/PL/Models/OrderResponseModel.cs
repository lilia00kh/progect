using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace PL.Models
{
    public class OrderResponseModel
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "First name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the last name is 60 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the city is 60 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        [RegularExpression(@"^\+380\d{2}\d{2}\d{2}\d{3}$", ErrorMessage = "Phone number must have form like: +380 ** ** ** ***")]
        public string Phone { get; set; }
        public string GoodDetails { get; set; }
        public string DeliveryDetails { get; set; }
        public string PaymentDetails { get; set; }
    }
}
