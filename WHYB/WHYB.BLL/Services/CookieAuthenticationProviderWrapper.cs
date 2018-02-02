using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using WHYB.BLL.Interfaces;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;

namespace WHYB.BLL.Services
{
    public static class CookieAuthenticationProviderWrapper 
    {
        public static ICookieAuthenticationProvider Provider()
        {
            return new CookieAuthenticationProvider
            {
                OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                    validateInterval: TimeSpan.FromMinutes(30),
                    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            };
        }
    }
}