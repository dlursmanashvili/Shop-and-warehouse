using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class CategoryRepositoryTest : RepositoryTestBase<Category, CategoryRepository>
    {
        // პროცედურები არის გასამართი
        private static object _categoryId;

        [Theory, TestPriority(1)]
        [InlineData("Unit", "Test")]
        public void MaintenanceCategoryInsert(string categoryName, string description)
            => Assert.NotNull(_categoryId = _repository.Insert(CreateCategory(categoryName, description)));

        [Fact]
        [TestPriority(2)]
        public void MaintenanceCategoryGet() => Assert.NotNull(_repository.Get(_categoryId));

        [Fact, TestPriority(3)]
        public void MaintenanceCategoryLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData("Updated", "UnitTest")]
        public void MaintenanceCategoryUpdate(string categoryName, string description)
        {
            _repository.Update(CreateCategory(categoryName, description, _categoryId));
            Category category = _repository.Get(_categoryId);
            Assert.True(category.CategoryName == categoryName && category.Description == description);
        }

        [Fact, TestPriority(5)]
        public void MaintenanceCategoryDelete()
        {
            _repository.Delete(_categoryId);
            Assert.True(_repository.Get(_categoryId) == default);
        }

        private Category CreateCategory(string categoryName, string description, object id = null)
        {
            Category category = new() { CategoryName = categoryName, Description = description };
            if (id != null) category.ID = id;
            return category;
        }
    }
}
