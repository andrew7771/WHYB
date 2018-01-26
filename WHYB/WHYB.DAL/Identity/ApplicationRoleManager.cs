using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store) : base(store)
        {
        }
    }
}