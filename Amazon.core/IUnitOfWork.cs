using Amazon.core.Entities;
using Amazon.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> complete();
    }
}
