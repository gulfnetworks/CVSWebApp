using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class SocialMedia
    {
        public int SocialMediaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }


        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
