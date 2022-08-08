using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class ContactInfoRepositoryTest : RepositoryTestBase<ContactInfo, ContactInfoRepository>
    {
        private static object _contactInfoId;

        [Theory, TestPriority(1)]
        [InlineData(1, 1, "unit-test@gmail.com", true)]
        public void MaintenanceContactInfoInsert(object employeeId, byte contactType, string contactData, bool isPrimary)
            => Assert.NotNull(_contactInfoId = _repository.Insert(CreatePermission(employeeId, contactType, contactData, isPrimary)));

        [Fact]
        [TestPriority(2)]
        public void MaintenanceContactInfoGet() => Assert.NotNull(_repository.Get(_contactInfoId));

        [Fact, TestPriority(3)]
        public void MaintenanceContactInfoLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData(1, 1, "updated-unit-test@gmail.com", true)]
        public void MaintenanceContactInfoUpdate(object employeeId, byte contactType, string contactData, bool isPrimary)
        {
            _repository.Update(CreatePermission(employeeId, contactType, contactData, isPrimary, _contactInfoId));
            ContactInfo contactInfo = _repository.Get(_contactInfoId);
            Assert.True(contactInfo.ContactType == contactType && contactInfo.ContactData == contactData && contactInfo.IsPrimary);
        }

        [Fact, TestPriority(5)]
        public void MaintenanceContactInfoDelete()
        {
            _repository.Delete(_contactInfoId);
            Assert.True(_repository.Get(_contactInfoId) == default);
        }

        private ContactInfo CreatePermission(object employeeId, byte contactType, string contactData, bool isPrimary, object id = null)
        {
            ContactInfo contactInfo = new() { EmployeeID = employeeId, ContactType = contactType, ContactData = contactData, IsPrimary = isPrimary };
            if (id != null) contactInfo.ID = id;
            return contactInfo;
        }
    }
}