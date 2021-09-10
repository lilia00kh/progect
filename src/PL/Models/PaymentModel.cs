using System;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid GoodId { get; set; }

        [Required(ErrorMessage = "Status is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the status is 60 characters.")]
        public string Status { get; set; }
    }
}
