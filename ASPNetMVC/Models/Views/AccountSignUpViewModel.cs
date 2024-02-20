using ASPNetMVC.Models.Components;
using ASPNetMVC.Models.Sections;

namespace ASPNetMVC.Models.Views;

public class AccountSignUpViewModel
{
    public string Title { get; set; } = "Ultimate Task Management";
    public SignUpViewModel SignUp { get; set; } = new SignUpViewModel
    {
        Id = "sign-up",
        Title = "Welcome Back",
        Text = "Already have an account?",
        Link = new LinkViewModel
        {
            ControllerName = "Account",
            ActionName = "SignIn",
            Text = "Sign in here"
        }
    };
}
