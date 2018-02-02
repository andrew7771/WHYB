using System;
using System.Threading.Tasks;

namespace WHYB.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
    }
}