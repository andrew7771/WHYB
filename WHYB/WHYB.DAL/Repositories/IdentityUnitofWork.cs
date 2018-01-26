using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext<ApplicationUser> _db;
        private readonly IDbSetRepositoryManager<ClientProfile> _clientProfileRepositoryManager;

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityUnitOfWork(string connectionString)
        {
            _db = new IdentityDbContext<ApplicationUser>(connectionString);
        }
        
        public IDbSetRepositoryManager<ClientProfile> ClientProfileRepositoryManager =>
            _clientProfileRepositoryManager ?? new DbSetRepositoryManager<ClientProfile>(_db);

        public UserManager<ApplicationUser> UserManager => _userManager ??
                                                        new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        
        public RoleManager<ApplicationRole> RoleManager => _roleManager ??
                                                        new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_db));
        

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