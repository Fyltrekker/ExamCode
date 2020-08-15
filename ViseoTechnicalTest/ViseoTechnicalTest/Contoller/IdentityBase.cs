using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ViseoTechnicalTest.Contoller
{
    public class IdentityBase: ControllerBase
    {     
        protected Service.Enums.UserType GetRole()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;    
            var role = claimsIdentity.FindFirst(ClaimTypes.Role);
            return (Service.Enums.UserType)Enum.Parse(typeof(Service.Enums.UserType), role.Value);
        }
    }
}
