using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Generic
{
    public interface ILogic<TEntity> where TEntity : class
    {
        long Count();
        TEntity Create(TEntity entity);
        bool Delete(int entityId);
        bool Delete(Guid entityId);
        ICollection<TEntity> GetAll();
        TEntity GetById(int entityId);
        TEntity GetById(Guid entityId);
        bool Update(int entityId, TEntity entity);

    }

    public interface ICollectionLogic<TEntity> : ILogic<TEntity>
        where TEntity : class
    {
        //void Create(ICollection<TEntity> entities);
        //bool Delete(List<int> entityIds);
        //bool Update(ICollection<TEntity> entities);
    }
}
