using EmployeeWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Interfaces
{
   public interface ICosmosDbService
   {
      Task<IEnumerable<Employee>> GetEmployeesAsync(string filter);
      Task AddEmployeeAsync(Employee employee);
      Task UpdateEmployeeAsync(Employee employee);
      Task DeleteEmployeeAsync(string id);
   }
}
