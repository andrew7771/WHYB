using System;
using System.Threading.Tasks;
using WHYB.DAL.Entities;
using WHYB.DAL.Identity;

namespace WHYB.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }

        IDbSetRepositoryManager<ClientProfile> ClientProfileRepositoryManager { get; }

        ApplicationRoleManager RoleManager { get; }

        Task SaveAsync();
    }
}