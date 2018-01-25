using System.Threading.Tasks;
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

        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationUserManager _userManager;

        public IdentityUnitOfWork(string connectionString)
        {
            _db = new IdentityDbContext<ApplicationUser>(connectionString);
        }

        public ApplicationUserManager UserManager => _userManager ??
                                                     new ApplicationUserManager(new UserStore<ApplicationUser>(_db));

        public ApplicationRoleManager RoleManager => _roleManager ??
                                                     new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));

        public IDbSetRepositoryManager<ClientProfile> ClientProfileRepositoryManager =>
            _clientProfileRepositoryManager ?? new DbSetRepositoryManager<ClientProfile>(_db);


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