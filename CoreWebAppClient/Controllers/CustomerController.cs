using CoreWebAppClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoreWebAppClient.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:5201/api/");
        HttpClient client;

        public CustomerController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAdress;
        }

        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync(baseAdress + "customers");

            List<PersonViewModel> customers = new List<PersonViewModel>();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                customers = JsonConvert.DeserializeObject<List<PersonViewModel>>(json);
            }

            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(personViewModel), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(baseAdress + "customers", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await client.GetAsync(baseAdress + string.Format("customers/{0}", id));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var customer = JsonConvert.DeserializeObject<PersonViewModel>(json);

                return View(customer);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PersonViewModel personViewModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(personViewModel), Encoding.UTF8, "application/json");

            var response = await client.PutAsync(baseAdress + string.Format("customers/{0}", personViewModel.Id), content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await client.DeleteAsync(baseAdress + string.Format("customers/{0}", id));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View();
        }
    }
}
