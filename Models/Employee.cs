using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmployeeWebAPI.Models
{
   /// <summary>
   /// Employee model
   /// </summary>
   [JsonObject(Title = "employees")]
   public class Employee
   {

      [JsonProperty(PropertyName = "id")]
      public string Id { get; set; }

      /// <summary>
      /// FirstName of the employee
      /// </summary>
      [JsonProperty(PropertyName = "firstName")]
      public string FirstName { get; set; }

      /// <summary>
      /// LastName of the employee
      /// </summary>
      [JsonProperty(PropertyName = "lastName")]
      public string LastName { get; set; }

      /// <summary>
      /// Age of the employee
      /// </summary>
      [JsonProperty(PropertyName = "age")]
      public int Age { get; set; }

      /// <summary>
      /// Salary of the employee
      /// </summary>
      [JsonProperty(PropertyName = "salary")]
      public string Salary { get; set; }
   }
}
