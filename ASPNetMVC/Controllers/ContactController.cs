using ASPNetMVC.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ASPNetMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(string response = "")
        {
            ViewData["Title"] = "Contact Us";
            ViewData["Response"] = response;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContacIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using var http = new HttpClient();
                using var content = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
                var response = await http.PostAsync("https://localhost:7269/api/contact?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { response = "Thank you for reaching out to us"});
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Index", new { response = "You are unauthorized to perform this acion" });
                }

                return RedirectToAction("Index", new { response = "An error ocurred, please try again" });
            }

            return RedirectToAction("Index", new { response = "Please provide all required information" });
        }
    }
}
