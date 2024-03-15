using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Models;

public class ProfileSecurityModel
{
    public string? ErrorMessage { get; set; }

    [Display(Name = "Current password", Prompt = "Enter your current password", Order = 0)]
    [Required(ErrorMessage = "Current password is required")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()\\-_=+{};:,<.>]).{8,}$", ErrorMessage = "Invalid password")]
    public string CurrentPassword { get; set; } = null!;


    [Display(Name = "New password", Prompt = "Enter your new password", Order = 1)]
    [Required(ErrorMessage = "New password is required")]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()\\-_=+{};:,<.>]).{8,}$", ErrorMessage = "Invalid password")]
    public string NewPassword { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Confirm your new password", Order = 2)]
    [Required(ErrorMessage = "Confirm new password is required")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Password doesn't match")]
    public string ConfirmPassword { get; set; } = null!;

    public ProfileDeleteModel? DeleteModel { get; set; }
}