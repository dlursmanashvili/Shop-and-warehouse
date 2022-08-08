using System;
using Store.Models;
using Store.Repositories;
using Xunit;

namespace Store.Tests.RepositoriesUnitTests
{
    [CollectionDefinition("SequentialRepositoriesTests", DisableParallelization = true)]
    [TestCaseOrderer("Store.Tests.PriorityOrderer", "Store.Tests")]
    public class EmployeeRepositoryTest : RepositoryTestBase<Employee, EmployeeRepository>
    {
        private static object _employeeId;

        [Theory, TestPriority(1)]
        [InlineData("Unit", "Test", "2000-01-01", "2000-01-01")]
        public void MaintenanceEmployeeInsert(string firstName, string lastName, string hireDate, string birthDate)
            => Assert.NotNull((_employeeId = _repository.Insert(CreateEmployee(firstName, lastName, hireDate, birthDate))));

        [Fact]
        [TestPriority(2)]
        public void MaintenanceEmployeeGet() => Assert.NotNull(_repository.Get(_employeeId));

        [Fact, TestPriority(3)]
        public void MaintenanceEmployeeLoad() => Assert.NotEmpty(_repository.Load());

        [Theory, TestPriority(4)]
        [InlineData("Updated", "UnitTest", "2000-01-01", "2000-01-01")]
        public void MaintenanceEmployeeUpdate(string firstName, string lastName, string hireDate, string birthDate)
        {
            _repository.Update(CreateEmployee(firstName, lastName, hireDate, birthDate, _employeeId));
            Employee employee = _repository.Get(_employeeId);
            Assert.True(employee.FirstName == firstName && employee.BirthDate == DateTime.Parse(birthDate) &&
                        employee.LastName == lastName && employee.HireDate == DateTime.Parse(hireDate)
            );
        }

        [Fact, TestPriority(5)]
        public void MaintenanceEmployeeDelete()
        {
            _repository.Delete(_employeeId);
            Assert.True(_repository.Get(_employeeId) == default);
        }

        private Employee CreateEmployee(string firstName, string lastName, string hireDate, string birthDate, object id = null)
        {
            Employee employee = new() { FirstName = firstName, LastName = lastName, HireDate = DateTime.Parse(hireDate), BirthDate = DateTime.Parse(birthDate) };
            if (id != null) employee.ID = id;
            return employee;
        }
    }
}