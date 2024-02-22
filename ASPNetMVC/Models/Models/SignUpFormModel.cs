using ASPNetMVC.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Models;

public class SignUpFormModel
{
    [Display(Name = "First Name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email Address", Prompt = "Enter your email address", Order = 2)]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password", Order = 3)]
    [Required(ErrorMessage = "Invalid password")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()\\-_=+{};:,<.>]).{8,}$", ErrorMessage = "Invalid password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [Required(ErrorMessage = "Password doesn't match")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "I aggre to the Terms & Conditions",  Order = 5)]
    [CheckboxRequired(ErrorMessage = "You must accept the terms and conditions")]
    public bool TermsAndCondition { get; set; } = false;
}
