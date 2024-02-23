using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Models;

public class ProfileAddressModel
{
    [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
    [Required(ErrorMessage = "Address line is required")]
    public string AddresLine1 { get; set; } = null!;

    [Display(Name = "Address line 2", Prompt = "Enter your second address line", Order = 1)]
    public string? AddresLine2 { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 0)]
    [Required(ErrorMessage = "Postal code is required")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;

    [Display(Name = "City", Prompt = "Enter your city", Order = 1)]
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = null!;
}
