using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOGIP_v2.Models
{
    public class CategoriaMonto
    {
        public int Id { get; set; }
        public RoleViewModel  IdCategoria { get; set; }
        public int monto { get; set; }
    }
}