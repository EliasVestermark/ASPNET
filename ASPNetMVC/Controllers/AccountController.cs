using ASPNetMVC.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            var viewModel = new AccountSignInViewModel();
            ViewData["Title"] = "Sign In";
            return View(viewModel);
        }

        public IActionResult SignUp()
        {
            var viewModel = new AccountSignUpViewModel();
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}
