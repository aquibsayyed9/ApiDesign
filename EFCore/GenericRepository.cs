using EzPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EzPay.EFCore
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _entities;
        protected readonly DbSet<T> _dbset;
        public GenericRepository(DbContext dbContext)
        {
            _entities = dbContext;
            _dbset = dbContext.Set<T>();
        }

        public virtual IQueryable<T> Table
        {

            get
            {
                return this._dbset;
            }
        }
        public virtual IQueryable<T> TableNoTracking
        {

            get
            {
                return this._dbset.AsNoTracking();
            }
        }

        public T Add(T entity)
        {
            return _dbset.Add(entity).Entity;
        }

        public T Delete(T entity)
        {
            return _dbset.Remove(entity).Entity;
        }

        public void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;

        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable<T>();
        }

        public void Save()
        {
            _entities.SaveChanges();

        }

        public IEnumerable<T> GetList(T entity)
        {
            PropertyInfo prop = null;
            object value = null;
            foreach (var item in typeof(T).GetProperties())
            {
                prop = item;
                value = item.GetValue(entity);
                if (value != null)
                {
                    if (value.GetType() == typeof(Int64) && Convert.ToInt64(value) <= 0)
                    {
                        continue;
                    }
                    else if (value.GetType() == typeof(Int32) && Convert.ToInt32(value) <= 0)
                    {
                        continue;
                    }

                    if (value.GetType() == typeof(Decimal) && Convert.ToDecimal(value) <= 0)
                    {
                        continue;
                    }
                    else if (value.GetType() == typeof(Double) && Convert.ToDouble(value) <= 0)
                    {
                        continue;
                    }
                    else if (value.GetType() == typeof(System.DateTime) && Convert.ToDateTime(value) <= DateTime.MinValue)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var parameterExpression = Expression.Parameter(typeof(T), "x");
            var constant = Expression.Constant(value);
            var property = Expression.Property(parameterExpression, prop.Name);
            var expression = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(expression, parameterExpression);
            //var result = _towerDetailsRepository.FindBy(lambda).ToList();
            var result = _dbset.Where(lambda.Compile()).AsEnumerable();
            return result;
        }

        public T Get(T entity)
        {
            var result = GetList(entity);
            return result.FirstOrDefault<T>();
        }
    }
}
