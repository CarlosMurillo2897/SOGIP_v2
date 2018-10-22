using SOGIP_v2.Models;
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
        public static string getIdentificador(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Id");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetCedula(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Cedula");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetNombre1(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Nombre1");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetSexo(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Sexo");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}