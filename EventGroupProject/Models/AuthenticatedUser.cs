using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class AuthenticatedUser
    {
        public string email { get; private set; }

        public AuthenticatedUser(IHttpContextAccessor contextAccessor)
        {
            email = contextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
