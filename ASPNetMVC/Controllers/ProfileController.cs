using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Views;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace ASPNetMVC.Controllers;

[Authorize]
public class ProfileController(UserManager<UserEntity> userManager, AddressService addressManager, SignInManager<UserEntity> signInManager, AppDbContext appDbContext) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressService _addressManager = addressManager;
    private readonly AppDbContext _context = appDbContext;

    [HttpGet]
    public async Task<IActionResult> Index(string message = "")
    {
        var viewModel = await PopulateProfileIndexAsync();
        viewModel.Id = "profile-details";
        viewModel.BasicInfo.ErrorMessage = message;
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Security(string message = "")
    {
        var viewModel = await PopulateProfileIndexAsync();
        viewModel.Id = "profile-security";
        viewModel.Security.ErrorMessage = message;
        return View("Index", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
        var viewModel = await PopulateProfileIndexAsync();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var courseIdList = new List<int>();

        if (userId != null)
        {
            var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);
            
            if (user!.Courses !=  null)
            {
                foreach (var course in user.Courses)
                {
                    courseIdList.Add(course.Id);
                }
            } 
        }

        if (courseIdList.Count > 0)
        {
            using var http = new HttpClient();

            foreach (var id in courseIdList)
            {
                var response = await http.GetAsync($"https://localhost:7269/api/course/{id}?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1&Id=");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<SingleCourseModel>(json);
                    viewModel.SavedCoursesModel.Courses.Add(data!);
                }
            }
        }

        viewModel.Id = "profile-saved-courses";
        return View("Index", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> BasicInfo(ProfileBasicInfoModel model)
    {
        if (TryValidateModel(model))
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.Bio = model.Bio;

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", new { message = "Success!" });
            }
        }

        return RedirectToAction("Index", new { message = "Invalid information, please try again" });
    }

    [HttpPost]
    public async Task<IActionResult> AddressInfo(ProfileAddressModel model)
    {
        if (TryValidateModel(model))
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var address = await _addressManager.GetAddressAsync(user.Id);

                if (address != null)
                {
                    address.AddressLine1 = model.AddresLine1;
                    address.AddressLine2 = model.AddresLine2;
                    address.PostalCode = model.PostalCode;
                    address.City = model.City;
                    await _addressManager.UpdateAddressAsync(address);
                }
                else
                {
                    address = new AddressEntity
                    {
                        UserId = user.Id,
                        AddressLine1 = model.AddresLine1,
                        AddressLine2 = model.AddresLine2,
                        PostalCode = model.PostalCode,
                        City = model.City
                    };

                    await _addressManager.CreateAddressAsync(address);
                    return RedirectToAction("Index", new { message = "Success!" });
                }
            }
        }

        return RedirectToAction("Index", new { message = "Invalid information, please try again" });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ProfileSecurityModel model)
    {
        if (TryValidateModel(model))
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                return RedirectToAction("Security", new { message = "Success!" });
            }
        }

        return RedirectToAction("Security", new { message = "Invalid information, please try again" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(ProfileSecurityModel model)
    {
        if (model.DeleteModel!.Delete)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(user);

                return RedirectToAction("SignIn", "Account");
            }
        }

        return RedirectToAction("Security", new { message = "Invalid information, please try again" });
    }

    public async Task<IActionResult> RemoveCourse(int courseId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);

        if (user! != null)
        {
            var courseToBeRemoved = user.Courses!.FirstOrDefault(x => x.Id == courseId);

            if (courseToBeRemoved != null)
            {
                user.Courses!.Remove(courseToBeRemoved);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("SavedCourses", "Profile");
    }

    public async Task<IActionResult> RemoveAllCourses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);

        if (user!.Courses! != null)
        {
            foreach (var course  in user.Courses!)
            {
                user.Courses.Remove(course);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("SavedCourses", "Profile");
    }

    private async Task<ProfileBasicInfoModel> PopulateProfileBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            return new ProfileBasicInfoModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Bio = user.Bio,
                ProfileImage = "/images/john.svg"
            };
        }

        return null!;
    }

    private async Task<ProfileAddressModel> PopulateAddressInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null)
        {
            var address = await _addressManager.GetAddressAsync(user.Id);

            if (address != null)
            {
                return new ProfileAddressModel
                {
                    AddresLine1 = address.AddressLine1,
                    AddresLine2 = address.AddressLine2,
                    PostalCode = address.PostalCode,
                    City = address.City
                };
            }
        }

        return null!;
    }

    private async Task<ProfileIndexViewModel> PopulateProfileIndexAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileIndexViewModel
        {
            BasicInfo = await PopulateProfileBasicInfoAsync(),
            AddressInfo = await PopulateAddressInfoAsync(),
            IsExternalAccount = user!.IsExternalAccount
        };
    }
}
