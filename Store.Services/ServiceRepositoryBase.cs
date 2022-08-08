using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Store.Repositories;

namespace Store.Services
{
    public abstract class ServiceRepositoryBase<TModel, TRepository> : ServiceBase<TModel>
        where TModel : class, new()
        where TRepository : RepositoryBase<TModel>
    {
        protected readonly TRepository _repository;

        public ServiceRepositoryBase(IConfiguration configuration)
        {
            _repository = Activator.CreateInstance(typeof(TRepository), configuration) as TRepository;
        }

        public virtual TModel Get(object id) => _repository.Get(id);

        public virtual IEnumerable<TModel> Load() => _repository.Load();

        public virtual object Insert(TModel item) => _repository.Insert(item);

        public virtual void Update(TModel item) => _repository.Update(item);

        public virtual void Delete(object id) => _repository.Delete(id);

        public virtual void BeginTransaction() => _repository.BeginTransaction();

        public virtual void CommitTransaction() => _repository.CommitTransaction();

        public virtual void RollbackTransaction() => _repository.RollbackTransaction();
    }
}