using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
    public class OrderModel
    {
        //public Guid Id { get; set; }
        public string User { get; set; }

        [Required(ErrorMessage = "Email is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
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
        public List<TreeForBasketModel> Trees { get; set; }
        public List<ToyForBasketModel> Toys { get; set; }
        public List<DeliveryModel> Deliveries { get; set; }
        public List<PaymentModel> Payments { get; set; }

        [Required(ErrorMessage = "Data is a required field.")]
        public DateTime Date { get; set; }
    }
}
