using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class AuthenticatedUser
    {
        public string Email { get; private set; }

        public AuthenticatedUser(IHttpContextAccessor contextAccessor)
        {
            Email = contextAccessor.HttpContext.User.Identity.Name;
        }
    }
}
