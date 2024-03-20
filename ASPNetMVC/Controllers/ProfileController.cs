using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Views;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly AddressService _addressManager;

    public ProfileController(UserManager<UserEntity> userManager, AddressService addressManager, SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _addressManager = addressManager;
        _signInManager = signInManager;
    }

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

        //viewModel.SavedCoursesModel = await GetCourses();

        viewModel.SavedCoursesModel = new ProfileSavedCoursesModel
        {
            Courses = new List<Course> {
                new Course
                {
                    ImageUrl = "/images/bookmark-example.svg",
                    Title = "Blender Character Creator v2.0 for Video Games Design",
                    BestSeller = "",
                    Author = "Ralph Edwards",
                    NewPrice = "$18.99",
                    OldPrice = "$27.99",
                    Sale = "sale",
                    Duration = "160 hours",
                    RatingPercent = "92%",
                    RatingLikes = "(3.1K)"
                },
                new Course
                {
                    ImageUrl = "/images/bookmark-example.svg",
                    Title = "How to go to sleep",
                    BestSeller = "Best-Seller",
                    Author = "Edwin Edwards",
                    NewPrice = "$18.99",
                    OldPrice = "",
                    Sale = "",
                    Duration = "160 hours",
                    RatingPercent = "92%",
                    RatingLikes = "(3.1K)"
                },
                new Course
                {
                    ImageUrl = "/images/bookmark-example.svg",
                    Title = "Blender Character Creator v2.0 for Video Games Design",
                    BestSeller = "",
                    Author = "Ralph Edwards",
                    NewPrice = "$18.99",
                    OldPrice = "$27.99",
                    Sale = "sale",
                    Duration = "160 hours",
                    RatingPercent = "92%",
                    RatingLikes = "(3.1K)"
                },
            }
        };

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

    private async Task<List<Course>> GetCourses()
    {
        return null;
    }
}
