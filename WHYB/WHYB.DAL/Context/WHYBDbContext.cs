using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.DBInitializer;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Context
{
    public class WhybDbContext : IdentityDbContext<ApplicationUser>
    {
        public WhybDbContext(string connectionString)
            : base(connectionString)
        {
           Database.SetInitializer(new WhybDBInitializer());
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}