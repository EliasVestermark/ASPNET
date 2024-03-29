using System.ComponentModel.DataAnnotations;

namespace ASPNetAPI.Models;

public class SignInForm
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
