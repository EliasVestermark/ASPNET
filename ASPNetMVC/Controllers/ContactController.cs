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
    }
}
