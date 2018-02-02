using System;
using WHYB.DAL.Entities;

namespace WHYB.DAL.Interfaces
{
    public interface IRepository<in TEntity> : IDisposable where TEntity : DbEntity
    {
        void Create(TEntity item);
    }
}