using ASPNetAPI.Models;
using ASPNetAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(SubscribeFormModel model)
    {
        if (model != null)
        {
            if (!await _context.Subscribers.AnyAsync(x => x.Email == model.Email))
            {
                var result = await _subscribersService.CreateSubscriber(model);

                if (result)
                {
                    return Created("", null);
                }
                else
                {
                    return Problem("Unable to create subscription");
                }
            }

            return Conflict("Your email is already subscribed");
        }

        return BadRequest();
    }
}
