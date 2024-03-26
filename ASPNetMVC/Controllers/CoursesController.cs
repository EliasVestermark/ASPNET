using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Sections;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace ASPNetMVC.Controllers;

[Authorize]
public class CoursesController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    [Route("/courses")]
    [HttpGet]
    public async Task<IActionResult> Courses()
    {
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7269/api/course?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<SingleCourseModel>>(json);
            var viewModel = new CoursesModel { Courses = data! };

            return View(viewModel);
        }
        else
        {
            return RedirectToAction("Error404", "Home");
        }
    }

    [Route("/singlecourse")]
    [HttpGet]
    public async Task<IActionResult> SingleCourse(string courseId)
    {
        using var http = new HttpClient();
        var response = await http.GetAsync($"https://localhost:7269/api/course/{courseId}?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1&Id=");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SingleCourseModel>(json);

            return View(data);
        }
        else
        {
            return RedirectToAction("Error404", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> SaveCourse(int courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);
            var courses = new List<CourseEntity>();
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

            courses.Add(course!);

            if (user!.Courses != null)
            {
                if (!user!.Courses!.Any(x => x.Id == courseId))
                {
                    user.Courses = courses;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                user!.Courses = courses;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Courses", "Courses");
    }
}
