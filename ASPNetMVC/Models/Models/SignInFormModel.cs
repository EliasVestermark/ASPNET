using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Models;

public class SignInFormModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email Address", Prompt = "Enter your email address", Order = 0)]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password", Order = 1)]
    [Required(ErrorMessage = "Invalid password")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me", Order = 2)]
    public bool RememberMe { get; set; } = false;
}
