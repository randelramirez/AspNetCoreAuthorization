using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
    [Authorize(Policy = PoliciesConstants.DEV)]
    public class ForDevController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
