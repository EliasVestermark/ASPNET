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
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<SingleCourseModel>>(json);

        var viewModel = new CoursesModel { Courses = data! };

        return View(viewModel);
    }

    [Route("/singlecourse")]
    [HttpGet]
    public IActionResult SingleCourse()
    {
        var viewModel = new SingleCourseModel
        {
            CourseTitle = "Fullstack Web Developer Course from Scratch",
            Tags = new List<string> { "Best-Seller", "Digital"},
            Subtitle = "Egestas feugiat lorem eu neque suspendisse ullamcorper scelerisque aliquam mauris.",
            Reviews = "(1.2K reviews)",
            Likes = "5k likes",
            Duration = "148 hours",
            Author = "Albert Flores",
            Description = "Suspendisse natoque sagittis, consequat turpis. Sed tristique tellus morbi magna. At vel senectus accumsan, arcu mattis id tempor. Tellus sagittis, euismod porttitor sed tortor est id. Feugiat velit velit, tortor ut. Ut libero cursus nibh lorem urna amet tristique leo. Viverra lorem arcu nam nunc at ipsum quam. A proin id sagittis dignissim mauris condimentum ornare. Tempus mauris sed dictum ultrices.",
            WhatYouLearn = new List<string> { "Sed lectus donec amet eu turpis interdum.", "Nulla at consectetur vitae dignissim porttitor.", "Phasellus id vitae dui aliquet mi.", "Integer cursus vitae, odio feugiat iaculis aliquet diam, et purus.", "In aenean dolor diam tortor orci eu." },
            Includes = new List<string> { "148 hours on-demand video", "18 articles", "25 downloadable resources", "Full lifetime access", "Certificate of completion" },
            NewPrice = "$28.99",
            OldPrice = "$49.00",
            ProgramDetails = new List<ProgramDetailsModel>
            {
                new ProgramDetailsModel { DetailTitle = "Introduction. Getting started", DetailDescription = "Nulla faucibus mauris pellentesque blandit faucibus non. Sit ut et at suspendisse gravida hendrerit tempus placerat."},
                new ProgramDetailsModel { DetailTitle = "The ultimate HTML developer: advanced HTML", DetailDescription = "Lobortis diam elit id nibh ultrices sed penatibus donec. Nibh iaculis eu sit cras ultricies. Nam eu eget etiam egestas donec scelerisque ut ac enim. Vitae ac nisl, enim nec accumsan vitae est."},
                new ProgramDetailsModel { DetailTitle = "CSS & CSS3: basic", DetailDescription = "Duis euismod enim, facilisis risus tellus pharetra lectus diam neque. Nec ultrices mi faucibus est. Magna ullamcorper potenti elementum ultricies auctor nec volutpat augue."},
                new ProgramDetailsModel { DetailTitle = "JavaScript basics for beginners", DetailDescription = "Morbi porttitor risus imperdiet a, nisl mattis. Amet, faucibus eget in platea vitae, velit, erat eget velit. At lacus ut proin erat."},
                new ProgramDetailsModel { DetailTitle = "Understanding APIs", DetailDescription = "Risus morbi euismod in congue scelerisque fusce pellentesque diam consequat. Nisi mauris nibh sed est morbi amet arcu urna. Malesuada feugiat quisque consectetur elementum diam vitae. Dictumst facilisis odio eu quis maecenas risus odio fames bibendum."},
                new ProgramDetailsModel { DetailTitle = "C# and .NET from beginner to advanced", DetailDescription = "Quis risus quisque diam diam. Volutpat neque eget eu faucibus sed urna fermentum risus. Est, mauris morbi nibh massa."}
            },
            AuthorDescription = "Dolor ipsum amet cursus quisque porta adipiscing. Lorem convallis malesuada sed maecenas. Ac dui at vitae mauris cursus in nullam porta sem. Quis pellentesque elementum ac bibendum. Nunc aliquam in tortor facilisis. Vulputate eget risus, metus phasellus. Pellentesque faucibus amet, eleifend diam quam condimentum convallis ultricies placerat. Duis habitasse placerat amet, odio pellentesque rhoncus, feugiat at. Eget pellentesque tristique felis magna fringilla.",
            Subscribers = "240k subscribers",
            Followers = "180k followers",
            AuthorIcon = "/images/albert-flores-logo.svg",
            AuthorImage = "/images/albert-flores.svg"
        };

        return View(viewModel);
    }
}
