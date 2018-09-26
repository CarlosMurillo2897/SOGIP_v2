using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace App.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetCedula(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Cedula");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}