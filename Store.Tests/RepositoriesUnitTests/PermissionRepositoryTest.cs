using System;
using System.Linq;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class PermissionRepositoryTest : RepositoryTestBase<Permission, PermissionRepository>
    {
        private static object _permissionId;

        [Theory, TestPriority(1)]
        [InlineData("Test12", 12, "Unit Test")]
        public void MaintenancePermissionInsert(string permissionName, short permissionCode, string description)
            => Assert.NotNull((_permissionId = _repository.Insert(CreatePermission(permissionName, permissionCode, description))));

        [Fact, TestPriority(2)]
        public void MaintenancePermissionGet() => Assert.NotNull(_repository.Get(_permissionId));

        [Fact, TestPriority(3)]
        public void MaintenancePermissionLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData("Test13", 13, "Updated Unit Test")]
        public void MaintenancePermissionUpdate(string permissionName, short permissionCode, string description)
        {
            _repository.Update(CreatePermission(permissionName, permissionCode, description, _permissionId));
            Permission permission = _repository.Get(_permissionId);
            Assert.True(permission.PermissionName == permissionName && permission.PermissionCode == permissionCode &&
                        permission.Description == description
            );
        }

        [Fact, TestPriority(5)]
        public void MaintenancePermissionDelete()
        {
            _repository.Delete(_permissionId);
            Assert.True(_repository.Get(_permissionId) == default);
        }

        private Permission CreatePermission(string permissionName, short permissionCode, string description, object id = null)
        {
            Permission permission = new() { PermissionName = permissionName, PermissionCode = permissionCode, Description = description };
            if (id != null) permission.ID = id;
            return permission;
        }
    }
}