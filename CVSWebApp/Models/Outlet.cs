using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class Outlet
    {
        public int OutletId { get; set; }
        public string OutletName { get; set; }
        public string OutletAddress { get; set; }
        public string OutletCountry { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        
    }
}
