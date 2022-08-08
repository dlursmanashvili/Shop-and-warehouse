using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class RoleRepositoryTest : RepositoryTestBase<Role, RoleRepository>
    {
        private static object _roleId;

        [Theory, TestPriority(1)]
        [InlineData("Unit", "Test")]
        public void MaintenanceRoleInsert(string roleName, string description)
            => Assert.NotNull((_roleId = _repository.Insert(CreateRole(roleName, description))));

        [Fact]
        [TestPriority(2)]
        public void MaintenanceRoleGet() => Assert.NotNull(_repository.Get(_roleId));

        [Fact, TestPriority(3)]
        public void MaintenanceRoleLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData("Updated", "UnitTest")]
        public void MaintenanceRoleUpdate(string roleName, string description)
        {
            _repository.Update(CreateRole(roleName, description, _roleId));
            Role role = _repository.Get(_roleId);
            Assert.True(role.RoleName == roleName && role.Description == description);
        }

        [Fact, TestPriority(5)]
        public void MaintenanceRoleDelete()
        {
            _repository.Delete(_roleId);
            Assert.True(_repository.Get(_roleId) == default);
        }

        private Role CreateRole(string roleName, string description, object id = null)
        {
            Role role = new() { RoleName = roleName, Description = description };
            if (id != null) role.ID = id;
            return role;
        }
    }
}
