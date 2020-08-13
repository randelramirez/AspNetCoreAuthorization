using AspNetCoreAuthorization.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Claims
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<MyAppUser>
    {
        public UserClaimsPrincipalFactory(UserManager<MyAppUser> userManager,
           IOptions<IdentityOptions> identityOptions) : base(userManager, identityOptions)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(MyAppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            // Add the user's locale in the claims (Got to about page)
            identity.AddClaim(new Claim(Claims.Types.Work, user.Work));
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()));

            // Get Claims from claims from [AspNetUserClaims] provided by ASP.NET Core Identity
            //identity.AddClaims(await this.UserManager.GetClaimsAsync(user));


            return identity;
        }
    }
}
