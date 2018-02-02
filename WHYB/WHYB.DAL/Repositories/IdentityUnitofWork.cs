using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        //private readonly IdentityDbContext<ApplicationUser> _db;
        private readonly DbContext _db;

        //public IdentityUnitOfWork(IdentityDbContext<ApplicationUser> identityDbContext)
        //{
        //    _db = identityDbContext;
        //}
        public IdentityUnitOfWork(DbContext identityDbContext)
        {
            _db = identityDbContext;
        }

        public Task SaveAsync()
        {
            return _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}