using System.Data.Entity;
using WHYB.DAL.Context;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WHYB.DAL.DBInitializer
{
    public class WhybDBInitializer : DropCreateDatabaseAlways<WhybDbContext>
    {
        protected override void Seed(WhybDbContext context)
        {

            context.Roles.Add(new IdentityRole { Name = "admin" });
            context.Roles.Add(new IdentityRole { Name = "user" });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}