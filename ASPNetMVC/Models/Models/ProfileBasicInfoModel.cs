using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Models;

public class ProfileBasicInfoModel
{
    public string? ErrorMessage { get; set; }

    public string UserId { get; set; } = null!;

    [DataType(DataType.ImageUrl)]
    public string? ProfileImage { get; set; }

    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Email address is required")]
    public string Email { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone", Prompt = "Enter your phone", Order = 3)]
    public string? Phone { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Bio", Prompt = "Add a short bio...", Order = 4)]
    public string? Bio { get; set; }
}
