using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace frontend.Pages
{
    public class calculateModel : PageModel
    {
        private readonly ILogger<calculateModel> _logger;
        public calculateModel(ILogger<calculateModel> logger)
        {
            _logger = logger;

        }
        [BindProperty]
        public Calculate Calculate { get; set; }

        [BindProperty]
        public int Operator { get; set; }
        public List<Calculate> Calculations { get; set; }
        public string Message { get; set; }
        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var calc = new Calculate
                    {
                        First = Calculate.First,
                        Second = Calculate.Second,
                        Sum = Calculate.Sum
                    };

                    if (Operator == 1)
                    {
                        var client = new RestClient("https://examfunctionfirst.azurewebsites.net/api");
                        var request = new RestRequest("/addition?");
                        request.AddJsonBody(calc);
                        var response = client.Post<Calculate>(request);
                        Message = response.Content;
                        _logger.LogInformation($"Successfull save");
                    }
                    else
                    {
                        var client = new RestClient("https://examfunctionfirst.azurewebsites.net/api");
                        var request = new RestRequest("/subtraction?");
                        request.AddJsonBody(calc);
                        var response = client.Post<Calculate>(request);
                        Message = response.Content;
                        _logger.LogInformation($"Successfull save");
                    }
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                
            }
        }
    }
}
