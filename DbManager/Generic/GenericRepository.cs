using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DbManager.Generic
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private bool _disposed;
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(HeroDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            DatabaseFacade = context.Database;
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        /// <summary>
        ///     Create single Entity in database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        ///     Create list of entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Create(ICollection<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public DatabaseFacade DatabaseFacade { get; }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        ///     Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached) DbSet.Attach(entityToDelete);
            DbSet.Remove(entityToDelete);
        }

        /// <summary>
        ///     generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual void Delete(Func<TEntity, bool> where)
        {
            var objects = DbSet.Where(where).AsQueryable();
            foreach (var obj in objects)
                DbSet.Remove(obj);
        }

        public virtual void Delete(ICollection<TEntity> entities)
        {
            foreach (var item in entities) DbSet.Remove(item);
        }

        /// <summary>
        ///     Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Generic method to check if entity exists
        /// </summary>
        public virtual bool Exists(object id)
        {
            return DbSet.Find(id) != null;
        }

        /// <summary>
        ///     Get Top 100 entities
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Get()
        {
            return DbSet.AsNoTracking().Take(100);
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(object id)
        {
            return GetByEntity(id);
        }

        /// <summary>
        ///     generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual TEntity Get(Func<TEntity, bool> where)
        {
            return DbSet.AsNoTracking().SingleOrDefault(where);
        }

        /// <summary>
        ///     Select only required columns from entities
        /// </summary>
        /// <typeparam name="TType">Type of output</typeparam>
        /// <param name="where">Search parameters</param>
        /// <param name="select">select statement</param>
        /// <returns>TType projection</returns>
        public virtual IQueryable<TType> Get<TType>(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TType>> select) where TType : class
        {
            return DbSet.AsNoTracking().Where(where).Select(select);
        }


        /// <summary>
        ///     Generic method to fetch all the records from db
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual TEntity GetByEntity(object entity)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Equals((TEntity) entity));
        }

        /// <summary>
        ///     Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            var entity = DbSet.Find(id);
            Context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        ///     generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where);
        }

        /// <summary>
        ///     generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where);
        }

        /// <summary>
        ///     Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public virtual TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().Single(predicate);
        }

        /// <summary>
        ///     Include multiple
        /// </summary>
        public IQueryable<TEntity> GetWithInclude(
            Expression<Func<TEntity,
                bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public virtual void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                var outputLines = new List<string>();
                outputLines.Add(e.Message);
                //TODO Add logger
                //System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }
        }

        /// <summary>
        ///     Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        ///     Update Patch style
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties)
        {
            //Ensure only modified fields are updated.
            var dbEntityEntry = Context.Entry(entity);
            if (updatedProperties.Any())
                //update explicitly mentioned properties
                foreach (var property in updatedProperties)
                    dbEntityEntry.Property(property).IsModified = true;
            else
                //no items mentioned, so find out the updated entries
                foreach (var property in dbEntityEntry.OriginalValues.Properties)
                {
                    var original = dbEntityEntry.OriginalValues.GetValue<object>(property);
                    var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                    if (original != null && !original.Equals(current))
                        dbEntityEntry.Property(property.Name).IsModified = true;
                }
        }

        /// <summary>
        ///     Update list of entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(ICollection<TEntity> entities)
        {
            foreach (var item in entities) Update(item);
        }

        /// <summary>
        ///     Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    Debug.WriteLine("Repository is being disposed");
                    Context.Dispose();
                }

            _disposed = true;
        }
    }
}