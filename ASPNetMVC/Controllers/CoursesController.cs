using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Sections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ASPNetMVC.Controllers;

[Authorize]
public class CoursesController : Controller
{
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
            var viewModel = new CoursesModel {Courses = data! };

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
}
