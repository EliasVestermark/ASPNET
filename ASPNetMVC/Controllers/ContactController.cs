using ASPNetMVC.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact Us";
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContacIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View(viewModel);
            }
            return View(viewModel);
        }
    }
}
