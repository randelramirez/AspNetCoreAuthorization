using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAuthorization.Authorization.Policies
{
    public static class PoliciesConstants
    {
        public const string AUTHENTICATED_USER = nameof(AUTHENTICATED_USER);

        public const string IT_PROFESSIONAL = nameof(IT_PROFESSIONAL);

        public const string DEV = nameof(DEV);

        public const string BA = nameof(BA);

        public const string OLD_ENOUGH = nameof(OLD_ENOUGH);

        public const string CAN_EDIT_DATA = nameof(CAN_EDIT_DATA);

        public const string CAN_ACCESS_CONTROLLER = nameof(CAN_ACCESS_CONTROLLER);

    }
}
