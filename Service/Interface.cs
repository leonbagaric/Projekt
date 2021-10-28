using Model.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface Interface<TEntity> : IDisposable where TEntity : ModelBase
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetByKeyAsync(params object[] keys);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetByParamAsync(/*DataSourceParameter sp*/);

        Task<int> SaveAsync(TEntity entityToSave);

        bool Exists(int id);

        bool Exists(params object[] keys);
    }
}
