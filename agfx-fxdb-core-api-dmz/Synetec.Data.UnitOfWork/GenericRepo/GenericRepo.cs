using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Synetec.Data.UnitOfWork.GenericRepo
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepo(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var query = GetQueryable(filter, includeProperties);

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public virtual IQueryable<TEntity> GetQueryable(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = "")
        {
            var query = GetQueryable(filter, includeProperties);

            return orderBy != null ? orderBy(query): query;
        }

        public virtual IList<TEntity> GetAllAsList()
        {
            return _dbSet.ToList();
        }

        //public virtual IQueryable<TEntity> GetAllAsQueryable()
        //{
        //    return _dbSet;
        //}

        public virtual TEntity GetByPrimaryKey(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (_context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);
            }
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void IgnoreProperty(TEntity entity, Expression<Func<TEntity, object>> property  )
        {
            //var prop = nameof(property);
            string prop = ConvertPropertyToString(property);

            _context.Entry(entity).Property(prop).IsModified = false;
        }

        /// <summary>
        /// This is legacy. Use C# 6 nameOf([property])
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string ConvertPropertyToString(Expression<Func<TEntity, object>> property)
        {
            var body = property.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression) property.Body).Operand as MemberExpression;
            }

            string prop = body.Member.Name;
            return prop;
        
        }

        private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        //*****************************Context disposal******************************

        //protected virtual void Dispose(bool disposing)
        //{
        //      _context = null;
        //    _dbSet = null;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
