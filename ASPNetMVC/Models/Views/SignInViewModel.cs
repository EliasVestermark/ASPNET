using ASPNetMVC.Models.Components;
using ASPNetMVC.Models.Models;

namespace ASPNetMVC.Models.Views
{
    public class SignInViewModel
    {
        public string Id { get; set; } = "sign-in";
        public string Title { get; set; } = "Welcome Back!";
        public string Text { get; set; } = "Don't have an account yet?";
        public LinkViewModel Link { get; set; } = new LinkViewModel
        {
            ControllerName = "Account",
            ActionName = "SignUp",
            Text = "Sign up here"
        };
        public SignInFormModel Form { get; set; } = new SignInFormModel();
    }
}
