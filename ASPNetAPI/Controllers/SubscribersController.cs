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
public class SubscribersController(AppDbContext context, SubscribersService subscribersService) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly SubscribersService _subscribersService = subscribersService;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _subscribersService.GetAllSubscribers();

        if (result.Any())
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var result = await _subscribersService.GetOneSubscriber(id);

        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, string email)
    {
        var result = await _subscribersService.GetOneSubscriber(id);

        if (result != null)
        {
            try
            {
                result.Email = email;
                _context.Update(result);
                await _context.SaveChangesAsync();

                return Ok(result);
            }
            catch
            {
                return Problem("Unable to update subscription");
            }
        }

        return NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        var result = await _subscribersService.GetOneSubscriber(id);

        if (result != null)
        {
            try
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return Problem("Unable to remove subscription");
            }
        }

        return NotFound();
    }
}
