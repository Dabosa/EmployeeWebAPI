using EmployeeWebAPI.Interfaces;
using EmployeeWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class EmployeeController : ControllerBase
   {
      ////connection object
      //private readonly IDocumentClient _documentClient;
      ////Set value as static in constructor so it cannot be changed
      //readonly String databaseid;
      //readonly String collectionid;
      ////Bring in config files
      //public IConfiguration Configuration { get; }
      //// GET: EmployeeController

      ////constructor
      //public EmployeeController(IDocumentClient documentClient, IConfiguration configuration)
      //{
      //   _documentClient = documentClient;
      //   Configuration = configuration;

      //   databaseid = Configuration["DatabaseId"];
      //   collectionid = "Employee";
      //   BuildCollection().Wait();
      //}

      ////create the DB and collection one time and leave it. (debug or prod)
      //private async Task BuildCollection()
      //{
      //   await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseid });
      //   //build collection and create db link
      //   await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(databaseid),
      //      new DocumentCollection { Id = collectionid });
      //}

      private readonly ICosmosDbService _cosmosDbService;
      public EmployeeController(ICosmosDbService cosmosDbService)
      {
         _cosmosDbService = cosmosDbService;
      }

      [HttpGet]
      public async Task<ActionResult> Get([FromQuery] string filter)
      {
         var result = await _cosmosDbService.GetEmployeesAsync(filter);
         return Ok(result);
      }

      [Route("Create")]
      [HttpPost]
      public async Task<ActionResult> Create([FromBody] Employee employee)
      {
         await _cosmosDbService.AddEmployeeAsync(employee);
         return Ok();
      }

      [Route("Delete/{id}")]
      [HttpDelete]
      public async Task<ActionResult> Delete([FromRoute] string id)
      {
         await _cosmosDbService.DeleteEmployeeAsync(id);
         return Ok();
      }

      [Route("Update")]
      [HttpPut]
      public async Task<ActionResult> Update([FromBody] Employee employee)
      {
         await _cosmosDbService.UpdateEmployeeAsync(employee);
         return Ok();
      }

   }
}

