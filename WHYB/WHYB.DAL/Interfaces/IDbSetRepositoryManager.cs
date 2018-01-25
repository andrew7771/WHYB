using WHYB.DAL.Entities;

namespace WHYB.DAL.Interfaces
{
    public interface IDbSetRepositoryManager<T> where T : DbEntity
    {
        void Create(T clientProfile);
    }
}