using Microsoft.AspNet.Identity.EntityFramework;

namespace WHYB.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}