using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class DbSetRepositoryManager<T> : IDbSetRepositoryManager<T> where T : DbEntity
    {
        private readonly IdentityDbContext<ApplicationUser> _identityDbContext;

        public DbSetRepositoryManager(IdentityDbContext<ApplicationUser> context)
        {
            _identityDbContext = context;
        }

        public void Create(T clientProfile)
        {
            _identityDbContext.Set<T>().Add(clientProfile);
            _identityDbContext.SaveChanges();
        }

        public void Dispose()
        {
            _identityDbContext.Dispose();
        }
    }
}