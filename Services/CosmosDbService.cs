using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Services
{
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using Microsoft.Azure.Cosmos;
   using Microsoft.Azure.Cosmos.Fluent;
   using Microsoft.Extensions.Configuration;
   using EmployeeWebAPI.Models;
   using EmployeeWebAPI.Interfaces;

   public class CosmosDbService : ICosmosDbService
   {
      private Container _container;

      public CosmosDbService(
          CosmosClient dbClient,
          string databaseName,
          string containerName)
      {
         this._container = dbClient.GetContainer(databaseName, containerName);
      }

      public async Task AddEmployeeAsync(Employee employee)
      {
         await this._container.CreateItemAsync<Employee>(employee, new PartitionKey(employee.Id));
      }

      public async Task DeleteEmployeeAsync(string id)
      {
         await this._container.DeleteItemAsync<Employee>(id, new PartitionKey(id));
      }

      public async Task<IEnumerable<Employee>> GetEmployeesAsync(string filter)
      {
         var queryString = "SELECT * FROM c";

         if(!string.IsNullOrEmpty(filter))
         {
            queryString = $"{queryString} where c.firstName LIKE '%{filter}%' or c.lastName LIKE '%{filter}%'";
         }

         var query = this._container.GetItemQueryIterator<Employee>(new QueryDefinition(queryString));
         List<Employee> results = new List<Employee>();
         while (query.HasMoreResults)
         {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
         }

         return results;
      }

      public async Task UpdateEmployeeAsync(Employee employee)
      {
         await this._container.UpsertItemAsync<Employee>(employee, new PartitionKey(employee.Id));
      }
   }
}
