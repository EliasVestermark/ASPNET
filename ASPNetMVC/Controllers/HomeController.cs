using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Sections;
using ASPNetMVC.Models.Views;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ASPNetMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string message = "", string fragment = "")
    {
        var viewModel = new HomeIndexViewModel();
        ViewData["Title"] = viewModel.Title;
        ViewData["Subscribe"] = message;
        ViewData["Fragment"] = fragment;

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe(NewsletterViewModel model)
    {
        if (ModelState.IsValid)
        {
            using var http = new HttpClient();
            using var content = new StringContent(JsonConvert.SerializeObject(model.SubscribeModel), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://localhost:7269/api/subscribers?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { message = "Thank you for subscribing", fragment = "#newsletter" });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", new { message = "The provided email is already subscribed", fragment = "#newsletter" });
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", new { message = "You are unauthorized to perform this acion", fragment = "#newsletter" });
            }
            return RedirectToAction("Index", new { message = "An error ocurred, please try again", fragment = "#newsletter" });
        }

        return RedirectToAction("Index", new { message = "Please provide a valid email", fragment = "#newsletter" });
    }

    [Route("/error")]
    public IActionResult Error404(int statusCode) => View();
}
