using AspNetCoreAuthorization.Authorization.Claims;
using AspNetCoreAuthorization.Authorization.Handlers;
using AspNetCoreAuthorization.Authorization.Policies;
using AspNetCoreAuthorization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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
            services.AddAuthorization(options =>
            {
                // Add policy: Add list of requirements, list of auth schemese
                options.AddPolicy("NoNameYet", new AuthorizationPolicy(
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

                options.AddPolicy(PoliciesConstants.CAN_ACCESS_CONTROLLER, policyBuilder =>
                {
                    policyBuilder.Requirements.Add(new MustBeInSoftwareDevelopmentRequirement());
                    policyBuilder.Requirements.Add(new MustBeUsingGmailRequirement());
                    policyBuilder.AuthenticationSchemes.Add(IdentityConstants.ApplicationScheme);
                });

                // Add policy: Using policy builder
                options.AddPolicy(PoliciesConstants.AUTHENTICATED_USER, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.AuthenticationSchemes = new List<string>
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                options.AddPolicy(PoliciesConstants.IT_PROFESSIONAL, policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimsConstants.WORK, ClaimsConstants.BA, ClaimsConstants.SOFTWARE_ENGINEER);
                    // Demo of adding Authentication scheme
                    policyBuilder.AuthenticationSchemes = new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                // DEV ONLY
                options.AddPolicy(PoliciesConstants.DEV, policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimsConstants.WORK, ClaimsConstants.SOFTWARE_ENGINEER);
                    // Demo of adding Authentication scheme
                    policyBuilder.AuthenticationSchemes = new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                // BA ONLY
                options.AddPolicy(PoliciesConstants.BA, policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimsConstants.WORK, ClaimsConstants.BA);
                    // Demo of adding Authentication scheme
                    policyBuilder.AuthenticationSchemes = new List<string>()
                    {
                        IdentityConstants.ApplicationScheme
                    };
                });

                options.AddPolicy(
                   PoliciesConstants.OLD_ENOUGH,
                   policyBuilder => policyBuilder.AddRequirements(
                       new MinimumAgeRequirement(18)
                   ));

                options.AddPolicy(PoliciesConstants.CAN_EDIT_DATA, policyBuilder => policyBuilder.AddRequirements(new IsToDoOwnerRequirement()));

            });

            // We use singleton because it doesn't have any dependency on its constructor
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

            // These two are handlers for  MustBeInSoftwareDevelopmentRequirement (A Requirement can have multiple handlers, 
            // but only need one handler to pass in order to satisfy the requirement)
            // but for requirements, all requirements should be satisfied. Requirements = AND; Handlers = OR
            services.AddSingleton<IAuthorizationHandler, IsBusinessAnalystHandler>();
            services.AddSingleton<IAuthorizationHandler, IsDeveloperHandler>();

            services.AddSingleton<IAuthorizationHandler, UserIsUsingGmailHandler>();

            // We need EF Core DbContext for usermanager, IsToDoOwenerHandler has Scoped dependencies
            services.AddScoped<IAuthorizationHandler, IsToDoOwenerHandler>();

            return services;
        }

        //public static ControllerActionEndpointConventionBuilder MustHavePolicies(this ControllerActionEndpointConventionBuilder builder, params string[] policyNames)
        //{
        //    return builder.RequireAuthorization(policyNames);
        //}
    }
}
