using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.DBInitializer;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Context
{
    public class WhybDbContext : IdentityDbContext<ApplicationUser>
    {
        static WhybDbContext()
        {
            Database.SetInitializer(new WhybDBInitializer());
        }

        public WhybDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}