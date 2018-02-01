using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Identity
{
    public sealed class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)  { }
    }
}