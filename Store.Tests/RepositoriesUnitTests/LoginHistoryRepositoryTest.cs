using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class LoginHistoryRepositoryTest : RepositoryTestBase<LoginHistory, LoginHistoryRepository>
    {
        private static object _loginHistoryId;

        [Theory, TestPriority(1)]
        [InlineData(1, "testuser", false)]
        public void MaintenanceLoginHistoryInsert(object userId, string username, bool isSuccessful)
            => Assert.NotNull(_loginHistoryId = _repository.Insert(CreateLoginHistory(userId, username, isSuccessful)));

        [Fact, TestPriority(2)]
        public void MaintenanceLoginHistoryGet() => Assert.NotNull(_repository.Get(_loginHistoryId));

        [Fact, TestPriority(3)]
        public void MaintenanceLoginHistoryLoad() => Assert.NotEmpty(_repository.Load());

        private LoginHistory CreateLoginHistory(object userId, string username, bool isSuccessful, object id = null)
        {
            LoginHistory loginHistory = new() { UserID = userId, Username = username, IsSuccessful = isSuccessful };
            if (id != null) loginHistory.ID = id;
            return loginHistory;
        }
    }
}