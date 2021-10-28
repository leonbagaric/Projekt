using DAL;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Model.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class EntityService<TEntity> : Interface<TEntity> where TEntity : ModelBase
    {
        private readonly masterContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EntityService(masterContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public bool Exists(int id)
        {
            return _dbSet.Any(e => e.Id == id);
        }

        public bool Exists(params object[] keys)
        {
            TEntity entity = _dbSet.Find(keys);
            return (entity != null);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetByKeyAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys);
        }

        public Task<IEnumerable<TEntity>> GetByParamAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync(TEntity entityToSave)
        {
            int numOfEntriesWritten = 0;
            //Validation
            if (entityToSave.State == State.Added)
                _dbSet.Add(entityToSave);
            else if (entityToSave.State == State.Deleted)
                _dbSet.Remove(entityToSave);
            else if (entityToSave.State == State.Modified)
                _dbSet.Update(entityToSave);

            if (entityToSave.State != State.Unchanged)
            {
                await _context.SaveChangesAsync();
                entityToSave.State = State.Unchanged;
            }
            return numOfEntriesWritten;
        }

        #region IDisposable Region

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EntityService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
