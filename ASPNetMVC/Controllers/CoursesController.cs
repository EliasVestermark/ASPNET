using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Sections;
using Azure;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace ASPNetMVC.Controllers;

[Authorize]
public class CoursesController(AppDbContext context, HttpClient http) : Controller
{
    private readonly AppDbContext _context = context;
    private readonly HttpClient _http = http;

    [Route("/courses")]
    [HttpGet]
    public async Task<IActionResult> Courses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

        #region ACCESSTOKEN TEST

        //This code snippet uses access token to access GetAll method in the API CourseController. Works only with non-external accounts (not Google and Facebook).

        if (!user!.IsExternalAccount)
        {
            if (HttpContext.Request.Cookies.TryGetValue("AccessToken", out var token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var authorizedResponse = await _http.GetAsync("https://localhost:7269/api/course?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1");

                if (authorizedResponse.IsSuccessStatusCode)
                {
                    var json = await authorizedResponse.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<IEnumerable<SingleCourseModel>>(json);
                    var viewModel = new CoursesModel { Courses = data! };

                    return View(viewModel);
                }
            }
        }

        #endregion

        var response = await _http.GetAsync("https://localhost:7269/api/course?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<SingleCourseModel>>(json);
            var viewModel = new CoursesModel { Courses = data! };

            return View(viewModel);
        }

        return RedirectToAction("Error404", "Home");
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
        var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);

        if (user!.Courses != null)
        {
            if (!user!.Courses!.Any(x => x.Id == courseId))
            {
                if (course != null)
                {
                    user.Courses.Add(course);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
            }
        }
        else
        {
            if (course != null)
            {
                user.Courses!.Add(course);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Courses", "Courses");
    }
}
