using AspNetCoreAuthorization.Authorization.Claims;
using AspNetCoreAuthorization.Authorization.Handlers;
using AspNetCoreAuthorization.Authorization.Policies;
using AspNetCoreAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Middleware
{
    public static  class MiddlewareExtensions
    {
        public static IServiceCollection ConfigureApplicationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(option =>
            {
                // Add policy: Add list of requirements, list of auth schemese
                option.AddPolicy("NoNameYet", new AuthorizationPolicy(
                    new List<IAuthorizationRequirement>()
                    {
                        new MinimumAgeRequirement(18),
                        new MustBeRandelOrGemRequirement()
                    },
                    new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                        //CookieAuthenticationDefaults.AuthenticationScheme
                    }
                ));

                // Add policy: Using policy builder
                option.AddPolicy(PoliciesConstants.AUTHENTICATED_USER, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AuthenticationSchemes = new List<string>
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                option.AddPolicy(PoliciesConstants.IT_PROFESSIONAL, policyBuilder =>
                {
                    policyBuilder.RequireClaim(Constants.WORK, Constants.BA, Constants.SOFTWARE_ENGINEER);
                    // Demo of adding Authentication scheme
                    policyBuilder.AuthenticationSchemes = new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                // DEV ONLY
                option.AddPolicy(PoliciesConstants.DEV, policyBuilder =>
                {
                    policyBuilder.RequireClaim(Constants.WORK, Constants.SOFTWARE_ENGINEER);
                    // Demo of adding Authentication scheme
                    policyBuilder.AuthenticationSchemes = new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                // BA ONLY
                option.AddPolicy(PoliciesConstants.BA, policyBuilder =>
                {
                    policyBuilder.RequireClaim(Constants.WORK, Constants.BA);
                    // Demo of adding Authentication scheme
                    policyBuilder.AuthenticationSchemes = new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                option.AddPolicy(
                   "OldEnough",
                   policyBuilder => policyBuilder.AddRequirements(
                       new MinimumAgeRequirement(18)
                   ));

            });

            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

            return services;
        }
    }
}
