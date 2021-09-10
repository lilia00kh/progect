using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
