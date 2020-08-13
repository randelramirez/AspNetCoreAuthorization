using AspNetCoreAuthorization.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Controllers
{

    public class TestAuthController : Controller
    {
        public IActionResult TestMe()
        {
            return Ok("Successful");
        }

        [Authorize("OldEnough")]
        public IActionResult AgeTest()
        {
            return Ok("Successful");
        }
    }
}
