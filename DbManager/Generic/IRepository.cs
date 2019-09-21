using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DbManager.Generic
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        DatabaseFacade DatabaseFacade { get; }
        int Count();
        void Create(TEntity entity);
        void Create(ICollection<TEntity> entities);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Delete(Func<TEntity, bool> where);
        void Delete(ICollection<TEntity> entities);
        bool Exists(object item);
        IQueryable<TEntity> Get();
        TEntity Get(object id);
        TEntity Get(Func<TEntity, bool> where);

        IQueryable<TType> Get<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class;

        IQueryable<TEntity> GetAll();
        TEntity GetByEntity(object entity);
        TEntity GetByID(object id);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where);
        TEntity GetSingle(Func<TEntity, bool> predicate);

        IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate,
            params string[] include);

        void SaveChanges();
        void Update(TEntity entity);
        void Update(ICollection<TEntity> entities);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties);
    }
}