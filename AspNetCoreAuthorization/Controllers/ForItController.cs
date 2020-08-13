using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
    [Authorize(Policy = PoliciesConstants.IT_PROFESSIONAL)]
    public class ForItController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
