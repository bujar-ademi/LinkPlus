using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkPlus.dal.Repository;
namespace LinkPlus.dal
{
    public class UnitOfWork : IDisposable
    {
        private DbContext context;

        public UnitOfWork(DbContext dbContext)
        {
            context = dbContext;
            context.Database.Log = Console.Write;
        }
        private Dictionary<Type, object> repos = new Dictionary<Type, object>();

        public IRepository<T> Repository<T>() where T: class
        {
            if (repos.Keys.Contains(typeof(T)))
                return repos[typeof(T)] as IRepository<T>;
            IRepository<T> repo = new DataRepository<T>(context);
            repos.Add(typeof(T), repo);
            return repo;
        }
        public void SaveChanges()
        {
            try
            {
                var aa = context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}", validationErrors.Entry.Entity.ToString(), validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                var dbEntityEntry = concurrencyException.Entries.First();
                dbEntityEntry.Reload();
                var dbPropertyValues = dbEntityEntry.GetDatabaseValues();
                dbEntityEntry.OriginalValues.SetValues(dbPropertyValues);
            }
            catch (DbUpdateException updateException)
            {
                if (updateException.InnerException != null)
                    //Debug.WriteLine(updateException.InnerException.Message);
                    foreach (var entry in updateException.Entries)
                    {
                        //Debug.WriteLine(entry.Entity);
                    }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region "Context dispose"
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
