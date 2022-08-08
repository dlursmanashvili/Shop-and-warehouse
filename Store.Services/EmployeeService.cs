using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class EmployeeService : ServiceRepositoryBase<Employee, EmployeeRepository>
    {
        public EmployeeService(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<Employee> LoadEmptyEmployees() => _repository.LoadEmptyEmployees();
    }
}