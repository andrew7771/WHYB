using Microsoft.AspNet.Identity;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}