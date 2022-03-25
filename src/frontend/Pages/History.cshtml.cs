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
    public class HistoryModel : PageModel
    {
        private readonly ILogger<HistoryModel> _logger;
        public HistoryModel(ILogger<HistoryModel> logger)
        {
            _logger = logger;

        }
        public List<Calculate> Calculations { get; set; }

        public void OnGet()
        {
            try
            {
                 var client = new RestClient("https://examfunctionfirst.azurewebsites.net/api");
                var request = new RestRequest("/calculations?");

                var response = client.Execute<List<Calculate>>(request);
                Calculations = response.Data;
                _logger.LogInformation($"Successfull read");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            
        }
    }
}
