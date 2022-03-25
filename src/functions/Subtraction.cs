using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Math.Function
{
    public static class Subtraction
    {
        [FunctionName("Subtraction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "subtraction")] HttpRequest req,
            [CosmosDB(
                databaseName: "calculator",
                collectionName: "mycalc",
                ConnectionStringSetting = "CosmosDbConnectionString"
            )]IAsyncCollector<object> calculate,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Calculate>(requestBody);

            var calc = new Calculate {
                First = input.First,
                Second = input.Second,
                Sum = 0,
                TimeCreated = DateTime.Now,
                Operand = "-"
            };

            var response = string.Empty;

            if(calc.First < calc.Second){
                calc.Sum = calc.Second - calc.First;
                response = $"{calc.Second} - {calc.First} = {calc.Sum}";
            }
            else {
                calc.Sum = calc.First - calc.Second;
                response = $"{calc.First} - {calc.Second} = {calc.Sum}";
            }

            await calculate.AddAsync(calc);
            return new OkObjectResult(response);
        }
    }
}
