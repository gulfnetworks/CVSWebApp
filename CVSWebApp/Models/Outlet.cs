using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class Outlet
    {
        public int OutletId { get; set; }

        [DisplayName("Outlet Name")]
        public string OutletName { get; set; }

        [DisplayName("Address")]
        public string OutletAddress { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        
    }
}
