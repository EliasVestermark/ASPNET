﻿using ASPNetAPI.Filters;
using ASPNetAPI.Models;
using ASPNetAPI.Services;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace ASPNetAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[UseApiKey]
public class CourseController(CourseService courseService, AppDbContext context) : ControllerBase
{
    private readonly CourseService _courseService = courseService;
    private readonly AppDbContext _context = context;

    [HttpPost]
    public async Task<IActionResult> Create(CourseModel model)
    {
        if (model != null)
        {
            if (!await _context.Courses.AnyAsync(x => x.CourseTitle == model.CourseTitle))
            {
                var result = await _courseService.CreateCourse(model);

                if (result)
                {
                    return Created("", null);
                }
                else
                {
                    return Problem("Unable to create course");
                }
            }

            return Conflict("A course with the same title already exists");
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _courseService.GetAllCourseModels();

        if (!result.IsNullOrEmpty())
        {
            return Ok(result);
        }
        
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var result = await _courseService.GetOneCourseModel(id);

        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateOne(int id)
    //{

    //}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        var result = await _courseService.GetOneCourseEntity(id);

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
                return Problem("Unable to remove course");
            }
        }

        return NotFound();
    }
}
