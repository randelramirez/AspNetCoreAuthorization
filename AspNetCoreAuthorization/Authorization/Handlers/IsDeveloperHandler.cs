using AspNetCoreAuthorization.Authorization.Claims;
using AspNetCoreAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Handlers
{
    public class IsDeveloperHandler : AuthorizationHandler<MustBeInSoftwareDevelopmentRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBeInSoftwareDevelopmentRequirement requirement)
        {
            if (context.User.FindFirstValue(Types.Work) == ClaimsConstants.SOFTWARE_ENGINEER)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
