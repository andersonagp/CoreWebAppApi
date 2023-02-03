using CoreWebAppClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CoreWebAppClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClient;

        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:5201/api/customers";

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        private async Task<List<CustomerViewModel>> GetCustomers()
        {

            // Get an instance of HttpClient from the factpry that we registered
            // in Startup.cs
            var client = _httpClient.CreateClient("API Client");

            // Call the API & wait for response. 
            // If the API call fails, call it again according to the re-try policy
            // specified in Startup.cs
            var result = await client.GetAsync(Baseurl);

            if (result.IsSuccessStatusCode)
            {
                // Read all of the response and deserialise it into an instace of
                // WeatherForecast class
                var content = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<CustomerViewModel>>(content);
            }

            return null;
        }

        public async Task<List<CustomerViewModel>> GetCustomers2()
        {
            using (var client = new HttpClient())
            {                
                var result = await client.GetAsync(Baseurl);

                if (result.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = result.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list
                    return JsonConvert.DeserializeObject<List<CustomerViewModel>>(response);
                }

                return null;
            }
        }

        public async Task<IActionResult> Index()
        {
            var customers = await GetCustomers2();

            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}