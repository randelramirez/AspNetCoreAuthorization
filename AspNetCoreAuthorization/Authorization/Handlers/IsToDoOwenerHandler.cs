using AspNetCoreAuthorization.Areas.Identity.Data;
using AspNetCoreAuthorization.Authorization.Requirements;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Handlers
{
    public class IsToDoOwenerHandler : AuthorizationHandler<IsToDoOwnerRequirement, ToDo>
    {
        private readonly UserManager<MyAppUser> userManager;

        public IsToDoOwenerHandler(UserManager<MyAppUser> userManager)
        {
            this.userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsToDoOwnerRequirement requirement, ToDo resource)
        {
            var appUser = await this.userManager.GetUserAsync(context.User);
            if (appUser == null)
            {
                return;
            }

            if (resource.CreatedBy == Guid.Parse(appUser.Id))
            {
                context.Succeed(requirement);
            }
        }
    }
}
