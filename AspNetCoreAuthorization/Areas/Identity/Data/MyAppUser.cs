using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAuthorization.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the MyAppUser class
    public class MyAppUser : IdentityUser
    {
        public DateTime BirthDate { get; set; }

        public string Work { get; set; }
    }
}
