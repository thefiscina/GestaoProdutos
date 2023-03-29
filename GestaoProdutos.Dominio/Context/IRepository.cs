using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestaoProduto.Dominio
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> Consultar(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null);
        List<TEntity> Listar(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = null, int? take = null);
        int Contar(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetById(int id);
        TEntity Save(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        public void RemoveRange(List<TEntity> entities);
        void SaveChanges();
    }
}