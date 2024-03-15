using ASPNetMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Models;

public class ProfileDeleteModel
{
    [Display(Name = "Yes, I want to delete my account.", Order = 0)]
    [CheckboxRequired(ErrorMessage = "You must confirm you want to delete your account")]
    public bool Delete { get; set; } = false;
}
