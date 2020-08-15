using AspNetCoreAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Handlers
{
    public class UserIsUsingGmailHandler : AuthorizationHandler<MustBeUsingGmailRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeUsingGmailRequirement requirement)
        {
            if (context.User.FindFirstValue(ClaimTypes.Name).ToLower().Contains("gmail"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
