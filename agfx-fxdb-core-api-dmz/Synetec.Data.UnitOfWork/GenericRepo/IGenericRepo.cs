using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Synetec.Data.UnitOfWork.GenericRepo
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IList<TEntity> GetAllAsList();

        //IQueryable<TEntity> GetAllAsQueryable();

        TEntity GetByPrimaryKey(object id);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void IgnoreProperty(TEntity entity, Expression<Func<TEntity, object>> property);
    }
}
