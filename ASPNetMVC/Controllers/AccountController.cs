using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Sections;
using ASPNetMVC.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers
{
    public class AccountController : Controller
    {
        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction("SignIn", "Account");
        }

        public IActionResult SignIn()
        {
            var viewModel = new AccountSignInViewModel();
            ViewData["Title"] = "Sign In";
            return View(viewModel);
        }      
    }
}
