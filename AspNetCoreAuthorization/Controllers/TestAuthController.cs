using AspNetCoreAuthorization.Authorization.Policies;
using AspNetCoreAuthorization.Models;
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

        private readonly IAuthorizationService authService;

        public TestAuthController(IAuthorizationService authService)
        {
            this.authService = authService;
        }

        public IActionResult TestMe()
        {
            return Ok("Successful");
        }

        // On top of the applied global policies, we also add OLD_ENOUGH
        [Authorize(PoliciesConstants.OLD_ENOUGH)]
        public IActionResult AgeTest()
        {
            return Ok("Successful");
        }


        public async Task<IActionResult> EditTest()
        {
            // Test data to mimic something from a database
            // Only gem can edit because she's the owner
            var gem = Guid.Parse("13f5d67d-2de3-4006-a720-5c9ec88f3753");
            var model = new ToDo();
            model.Id = 1;
            model.CreatedBy = gem;

            // We can store the result if the user can edit or not, 
            // With this mechannism we can either return a Forbidden result or store it in a model and disable controls on the UI

            var canEdit = await this.authService.AuthorizeAsync(this.User, model, PoliciesConstants.CAN_EDIT_DATA);
            if (!canEdit.Succeeded)
            {
                return Forbid();
            }

            return Ok(canEdit);
        }
    }
}
