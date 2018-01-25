using System.Data.Entity;
using WHYB.DAL.Context;

namespace WHYB.DAL.DBInitializer
{
    public class WhybDBInitializer : DropCreateDatabaseAlways<WhybDbContext>
    {
        protected override void Seed(WhybDbContext context)
        {
            base.Seed(context);
        }
    }
}