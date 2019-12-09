using AutoMapper;
using DbManager.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Generic
{
    public class GenericLogic<TRepository, TBusinessEntity, TEntity> : ILogic<TBusinessEntity>
        where TRepository : IRepository<TEntity>
        where TBusinessEntity : class
        where TEntity : class
    {
        protected TRepository Repository;
        private static ILogger<GenericLogic<TRepository, TBusinessEntity, TEntity>> _logger;

        public GenericLogic(TRepository repository, ILogger<GenericLogic<TRepository, TBusinessEntity, TEntity>> logger)
        {
            Repository = repository;
            _logger = logger;
        }

        public virtual long Count()
        {
            return Repository.Count();
        }

        public virtual TBusinessEntity Create(TBusinessEntity entity)
        {
            using (var scope = Repository.DatabaseFacade.BeginTransaction())
            {
                try
                {
                    var item = Mapper.Map<TEntity>(entity);
                    Repository.Create(item);
                    Repository.SaveChanges();
                    scope.Commit();
                    var businessItem = Mapper.Map<TBusinessEntity>(item);
                    return businessItem;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }
            }
        }

        public virtual bool Delete(int entityId)
        {
            var success = false;
            if (entityId > 0)
                using (var scope = Repository.DatabaseFacade.BeginTransaction())
                {
                    var item = Repository.GetByID(entityId);
                    if (item != null)
                    {
                        Repository.Delete(item);
                        Repository.SaveChanges();
                        scope.Commit();
                        success = true;
                    }
                }

            return success;
        }

        public virtual bool Delete(Guid entityId)
        {
            var success = false;
            using (var scope = Repository.DatabaseFacade.BeginTransaction())
            {
                var item = Repository.GetByID(entityId);
                if (item != null)
                {
                    Repository.Delete(item);
                    Repository.SaveChanges();
                    scope.Commit();
                    success = true;
                }
            }

            return success;
        }

        public virtual ICollection<TBusinessEntity> GetAll()
        {
            var entities = Repository.GetAll();
            var result = new List<TBusinessEntity>();

            if (entities.Any()) result = Mapper.Map<List<TEntity>, List<TBusinessEntity>>(entities.ToList());

            return result;
        }

        public virtual TBusinessEntity GetById(int id)
        {
            var product = Repository.GetByID(id);
            var productModel = Mapper.Map<TBusinessEntity>(product);
            return productModel;
        }
        public virtual TBusinessEntity GetById(Guid guid)
        {
            var product = Repository.GetByID(guid);
            var productModel = Mapper.Map<TBusinessEntity>(product);
            return productModel;
        }

        public virtual bool Update(int entityId, TBusinessEntity entity)
        {
            var success = false;
            if (entity != null)
                using (var scope = Repository.DatabaseFacade.BeginTransaction())
                {
                    var product = Repository.GetByID(entityId);
                    if (product != null)
                    {
                        var item = Mapper.Map(entity, product);
                        Repository.Update(item);
                        Repository.SaveChanges();
                        scope.Commit();
                        success = true;
                    }
                }

            return success;
        }

        public virtual bool Update(Guid entityGuid, TBusinessEntity entity)
        {
            var success = false;
            if (entity != null)
                using (var scope = Repository.DatabaseFacade.BeginTransaction())
                {
                    var product = Repository.GetByID(entityGuid);
                    if (product != null)
                    {
                        var item = Mapper.Map(entity, product);
                        Repository.Update(item);
                        Repository.SaveChanges();
                        scope.Commit();
                        success = true;
                    }
                }

            return success;
        }

        public void Create(ICollection<TEntity> entities)
        {
            using (var scope = Repository.DatabaseFacade.BeginTransaction())
            {
                try
                {
                    var items = Mapper.Map<List<TEntity>>(entities);
                    foreach (var item in items)
                    {
                        Repository.Create(item);
                        Repository.SaveChanges();
                    }
                    scope.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
