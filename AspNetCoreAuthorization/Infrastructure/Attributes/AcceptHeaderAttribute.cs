using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AcceptHeaderAttribute : Attribute, IActionConstraint
    {
        private readonly string aceeptHeader;

        public int Order { get; set; }

        public AcceptHeaderAttribute(string acceptHeader)
        {
            this.aceeptHeader = acceptHeader;
        }

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Headers["Accept"].Any(h => h.IndexOf(aceeptHeader) >= 0);
        }
    }
}
