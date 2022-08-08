using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class UserRepositoryTest : RepositoryTestBase<User, UserRepository>
    {
        private static int _userId = 14;

        [Theory, TestPriority(1)]
        [InlineData("UnitTest3", true, "pass1")]
        public void MaintenanceUserInsert(string username, bool isActive, string password)
            => Assert.NotNull(_repository.Insert(CreateUser(username, isActive, password)));

        [Fact, TestPriority(2)]
        public void MaintenanceUserGet() => Assert.NotNull(_repository.Get(_userId));

        [Fact, TestPriority(3)]
        public void MaintenanceUserLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData("UpdatedTest1", true)]
        public void MaintenanceUserUpdate(string username, bool isActive)
        {
            _repository.Update(CreateUser(username, isActive));
            User user = _repository.Get(_userId);
            Assert.True(user.Username == username && user.IsActive == isActive);
        }

        [Fact, TestPriority(5)]
        public void MaintenanceUserDelete()
        {
            _repository.Delete(_userId);
            Assert.True(_repository.Get(_userId) == default);
        }

        private User CreateUser(string usernmae, bool isActive, string password = default)
        {
            User user = new() { ID = _userId, Username = usernmae, Password = password, IsActive = isActive };
            if (password != default) user.Password = password;
            return user;
        }
    }
}