using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Context;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class IdentityUnitofWork : IUnitOfWork
    {
        private WhybDbContext _db;

        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
        private IClientManager _clientManager;

         public IdentityUnitofWork(string connectionString)
        {
            _db = new WhybDbContext(connectionString);
        }

        public ApplicationUserManager UserManager => _userManager ?? new ApplicationUserManager(new UserStore<ApplicationUser>(_db));

        public IClientManager ClientManager => _clientManager ?? new ClientManager(_db);

        public ApplicationRoleManager RoleManager => _roleManager ?? new ApplicationRoleManager(new RoleStore<ApplicationRole>(_db));

         
        public void Dispose()
        {
            _db.Dispose();
        }
        
        public Task SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}
