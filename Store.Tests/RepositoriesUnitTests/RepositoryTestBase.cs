using System;
using Store.Repositories;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    public abstract class RepositoryTestBase<TModel, TRepository> : UnitTestBase
        where TModel : class, new()
        where TRepository : RepositoryBase<TModel>
    {
        protected readonly TRepository _repository;

        public RepositoryTestBase()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            _repository = Activator.CreateInstance(typeof(TRepository), configuration) as TRepository;
        }
    }
}