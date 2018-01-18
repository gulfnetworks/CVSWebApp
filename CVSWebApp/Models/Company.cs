using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class Company
    {
      
        public int CompanyId { get; set; }

        [DisplayName("Company")]
        public string CompanyName { get; set; }

        [DisplayName("Address")]
        public string CompanyAddress { get; set; }

        [DisplayName("Contact")]
        public string CompanyContact { get; set; }

        [DisplayName("Logo")]
        public string CompanyLogoUrl { get; set; }

        [DisplayName("Company Code")]
        public string CompanyCode { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [DisplayName("CountryId")]
        public int CountryId { get; set; }
        public  Country Country { get; set; }

        [DisplayName("Time Zone")]
        public string TimeZone { get; set; }

        public ICollection<Payment> Payments { get; set; }

    }
}
