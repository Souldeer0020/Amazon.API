using Amazon.core;
using Amazon.core.Entities;
using Amazon.core.Repositories;
using Amazon.repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;

        private Hashtable _repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<int> complete()
        {
            return await _storeContext.SaveChangesAsync();
        }
        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repositories is null)
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_storeContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity>;

        }

        public async ValueTask DisposeAsync()
        {
            await _storeContext.DisposeAsync();
        }


    }
}
