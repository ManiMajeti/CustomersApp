using CustomersApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CustomersApp.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7013/api/");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("customer");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadAsAsync<IEnumerable<Customer>>();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            var response = await _httpClient.PostAsJsonAsync("customer", customer);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"customer/{id}");
            response.EnsureSuccessStatusCode();
            var customer = await response.Content.ReadAsAsync<Customer>();
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"customer/{id}", customer);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"customer/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
