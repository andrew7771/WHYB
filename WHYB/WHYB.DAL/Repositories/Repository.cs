using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WHYB.DAL.Entities;
using WHYB.DAL.Interfaces;

namespace WHYB.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
    {
        //private readonly IdentityDbContext<ApplicationUser> _identityDbContext;
        private readonly DbContext _identityDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _identityDbContext = context;
            _dbSet = _identityDbContext.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            //_identityDbContext.SaveChanges();
        }

       
    }
}