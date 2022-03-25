using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Linq;
using Microsoft.Azure.Documents;

namespace Math.Function
{
    public static class Calculation
    {
        [FunctionName("Calculation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculations")] HttpRequest req,
            [CosmosDB(
                databaseName: "calculator",
                collectionName: "mycalc",
                ConnectionStringSetting = "CosmosDbConnectionString"
            )] DocumentClient client,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("calculator", "mycalc");
            IDocumentQuery<Calculate> query = client.CreateDocumentQuery<Calculate>(collectionUri, new FeedOptions { EnableCrossPartitionQuery = true })
                .OrderByDescending(p => p.TimeCreated).Take(5).AsDocumentQuery();

            return new OkObjectResult(query);
        }
    }
}
