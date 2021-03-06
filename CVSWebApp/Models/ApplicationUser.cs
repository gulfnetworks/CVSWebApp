﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVSWebApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<int>
    {
        [Display(Name = "User Name")]
        public override string UserName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName {
            get {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }



        [Display(Name = "Manager ID")]
        public int ManagerId { get; set; }


        [Display(Name = "Company ID")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }


        public int Active { get; set; }


        [ForeignKey("UserOutletId")]
        public int UserOutletId { get; set; }
  


        [InverseProperty("UserManager")]
        public virtual ICollection<Survey> SurveyManagers { get; set; }

        [InverseProperty("UserStaff")]
        public virtual ICollection<Survey> SurveyStaffs { get; set; }

    }
}
