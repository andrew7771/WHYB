using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Context
{
    public class WhybDbContext : IdentityDbContext<ApplicationUser>
    {
        public WhybDbContext(string connectionString) : base(connectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
