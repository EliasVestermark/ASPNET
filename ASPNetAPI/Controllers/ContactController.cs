using ASPNetAPI.Filters;
using ASPNetAPI.Models;
using ASPNetAPI.Services;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController(ContactService contactService) : ControllerBase
{
    private readonly ContactService _contactService = contactService;

    [HttpPost]
    public async Task<IActionResult> Create(ContactFormModel model)
    {
        if (model != null)
        {
            var result = await _contactService.CreateContact(model);

            if (result)
            {
                return Created("", null);
            }
            else
            {
                return Problem("Unable to create subscription");
            }
        }

        return BadRequest();
    }
}
