using ASPNetMVC.Models.Components;
using ASPNetMVC.Models.Models;

namespace ASPNetMVC.Models.Sections;

public class SignUpViewModel
{
    public string Id { get; set; } = "sign-up";
    public string Title { get; set; } = "Create Account";
    public string Text { get; set; } = "Already have an account?";
    public LinkViewModel Link { get; set; } = new LinkViewModel
    {
        ControllerName = "Account",
        ActionName = "SignIn",
        Text = "Sign in here"
    };
    public SignUpFormModel Form { get; set; } = new SignUpFormModel();
}
