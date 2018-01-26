using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;

namespace WHYB.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<ApplicationUser> UserManager { get; }

        IDbSetRepositoryManager<ClientProfile> ClientProfileRepositoryManager { get; }

        RoleManager<ApplicationRole> RoleManager { get; }

        Task SaveAsync();
    }
}