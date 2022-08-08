using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class ProductRepositoryTest : RepositoryTestBase<Product, ProductRepository>
    {
        private static object _productId;

        [Theory, TestPriority(1)]
        [InlineData(3, "Unit", "Test", 10)]
        public void MaintenanceProductInsert(object categoryId, string productName, string description, decimal price)
            => Assert.NotNull(_productId = _repository.Insert(CreateProduct(categoryId, productName, description, price)));

        [Fact]
        [TestPriority(2)]
        public void MaintenanceProductGet() => Assert.NotNull(_repository.Get(_productId));

        [Fact, TestPriority(3)]
        public void MaintenanceProductLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData(3, "Updated", "UnitTest", 11.9)]
        public void MaintenanceProductUpdate(object categoryId, string productName, string description, decimal price)
        {
            _repository.Update(CreateProduct(categoryId, productName, description, price, _productId));
            Product product = _repository.Get(_productId);
            Assert.True(product.ProductName == productName && product.Description == description && product.Price == price);
        }

        [Fact, TestPriority(5)]
        public void MaintenanceProductDelete()
        {
            _repository.Delete(_productId);
            Assert.True(_repository.Get(_productId) == default);
        }

        private Product CreateProduct(object categoryId, string productName, string description, decimal price, object id = null)
        {
            Product product = new() { CategoryID = categoryId, ProductName = productName, Description = description, Price = price };
            if (id != null) product.ID = id;
            return product;
        }
    }
}