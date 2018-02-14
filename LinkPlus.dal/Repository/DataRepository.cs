using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LinkPlus.dal.Repository
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> dbSet;

        public DataRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("Null DbContext");
            context = dbContext;
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
       
        public void AddRange(IList<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Attach(T entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return dbSet.Where(predicate);
            return dbSet.AsEnumerable();
        }
    }
}
