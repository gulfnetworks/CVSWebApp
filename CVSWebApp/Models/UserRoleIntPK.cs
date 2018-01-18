using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVSWebApp.Models
{
    public class UserRoleIntPK : IdentityRole<int>
    {
        public UserRoleIntPK() : base()  {}

        public UserRoleIntPK(string RoleName) : base()
        {
            Name = RoleName;
        }
    }
}
