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
        private WhybDbContext _db;
        private ApplicationRoleManager _roleManger;
        private ApplicationUserManager _userManager;
        private IClientManager _clientManager;

        public ApplicationUserManager UserManager => _userManager;
        public IClientManager ClientManager => _clientManager;
        public ApplicationRoleManager RoleManager => _roleManger;

        public IdentityUnitofWork(string connectionString)
        {
            _db = new WhybDbContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>());
            _roleManger = new ApplicationRoleManager(new RoleStore<ApplicationRole>());
            _clientManager = new ClientManager(_db);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = true;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    _userManager.Dispose();
                    _roleManger.Dispose();
                    _clientManager.Dispose();
                }
                this.disposed = true;
            }
        }

       
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
