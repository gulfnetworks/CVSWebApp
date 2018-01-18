using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public float Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int CompanyId { get; set; }
    }
}
