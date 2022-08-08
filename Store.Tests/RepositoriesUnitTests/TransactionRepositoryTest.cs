using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class TransactionRepositoryTest : RepositoryTestBase<Transaction, TransactionRepository>
    {
        private static object _transactionId;

        [Theory, TestPriority(1)]
        [InlineData(1, 1)]
        public void MaintenanceTransactionInsert(object userId, short transactionType)
            => Assert.NotNull(_transactionId = _repository.Insert(CreateTransaction(userId, transactionType)));

        [Fact, TestPriority(2)]
        public void MaintenanceTransactionGet() => Assert.NotNull(_repository.Get(_transactionId));

        [Fact, TestPriority(3)]
        public void MaintenanceTransactionLoad() => Assert.NotEmpty(_repository.Load());

        private Transaction CreateTransaction(object userId, short transactionType, object id = null)
        {
            Transaction transaction = new() { UserID = userId, TransactionType = transactionType };
            if (id != null) transaction.ID = id;
            return transaction;
        }
    }
}
