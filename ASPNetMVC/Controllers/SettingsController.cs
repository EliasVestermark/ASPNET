using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers;

public class SettingsController : Controller
{
    [Route("/settings")]
    public IActionResult ChangeTheme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(30)
        };

        Response.Cookies.Append("ThemeMode", mode, option);
        return Ok();
    }

    [HttpPost]
    [Route("/consent")]
    public IActionResult CookieConsent()
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(365),
            HttpOnly = true,
            Secure = true
        };

        Response.Cookies.Append("CookieConsent", "true", option);
        return Ok();
    }
}
