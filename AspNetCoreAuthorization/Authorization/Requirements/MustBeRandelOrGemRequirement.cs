using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Requirements
{
    public class MustBeRandelOrGemRequirement : IAuthorizationRequirement
    {
        public MustBeRandelOrGemRequirement()
        {
            // perhaps use IHttpContextAccesssor and get claims username/id 
        }

    }
}
