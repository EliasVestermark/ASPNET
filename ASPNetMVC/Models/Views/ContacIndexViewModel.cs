using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Views;

public class ContacIndexViewModel
{
    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Invalid name")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email")]
    [Display(Name = "Email Address", Prompt = "Enter your email address", Order = 1)]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;
   
    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Write your message", Order = 2)]
    [Required(ErrorMessage = "Message can't be empty")]
    public string Message { get; set; } = null!;
}
