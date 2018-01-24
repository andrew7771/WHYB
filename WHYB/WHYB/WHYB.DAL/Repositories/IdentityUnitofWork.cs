using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Context;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class IdentityUnitofWork : IUnitOfWork
    {
        private WhybDbContext db;
        private ApplicationRoleManager _roleManger;
        private ApplicationUserManager _userManager;
        private IClientManager _clientManager;

        public IdentityUnitofWork(string connectionString)
        {
            db = new WhybDbContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>());
            _roleManger = new ApplicationRoleManager(new RoleStore<ApplicationRole>());
            _clientManager = new ClientManager(db);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ApplicationUserManager UserManager { get; }
        public IClientManager ClientManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
