using EzPay.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EzPay.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        /// <summary>
        /// Find a entity by passing conditions
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add an Entity to Database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);

        /// <summary>
        /// Delete an entity from Database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Delete(T entity);
        /// <summary>
        /// Update an Entity
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);
        /// <summary>
        /// Save all changes whihc has been done in Database
        /// </summary>
        void Save();
        IEnumerable<T> GetList(T entity);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        T Get(T entity);
    }
}
