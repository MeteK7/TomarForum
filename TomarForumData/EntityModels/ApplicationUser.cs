using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomarForumData.EntityModels
{
    public class ApplicationUser:IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        public string UserDescription { get; set; } //Change the order of the properties later in order to be organized.
        public string ProfileImageUrl { get; set; }
        public int Rating { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime MembershipCreatedOn { get; set; }
    }
}

//It is necessary to add property to the class named "ApplicationUser" for every data related with the user.