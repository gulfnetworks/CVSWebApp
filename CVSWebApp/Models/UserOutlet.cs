using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class UserOutlet
    {
        public int UserOutletId { get; set; }
        public int OutletId { get; set; }
        public bool DefaultOutlet { get; set; }
        public int UserId { get; set; }
    }
}
