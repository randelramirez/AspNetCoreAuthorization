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
    /*
    NOTE:   If Authorization is globally on the middleware then the policies defined there will always be applied,
            but if we add an override here like [Authorize(Policy ="NewPolicy")] then that policy will be applied
            together with the ones defined globally in the middleware.
            Adding an Authorized attribute here results in an AND and not an OR
            Global Policies AND "NewPolicy"
    */
    public class TestAuthController : Controller
    {
        public IActionResult TestMe()
        {
            return Ok("Successful");
        }

        [Authorize(PoliciesConstants.OLD_ENOUGH)]
        public IActionResult AgeTest()
        {
            return Ok("Successful");
        }
    }
}
