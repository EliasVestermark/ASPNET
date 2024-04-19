using ASPNetMVC.Models.Models;
using ASPNetMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ASPNetMVC.Controllers;

[Authorize]
public class ProfileController(ProfileManager profileManager) : Controller
{
    private readonly ProfileManager _profileManager = profileManager;

    [HttpGet]
    public async Task<IActionResult> Index(string message = "")
    {
        var viewModel = await _profileManager.PopulateProfileIndexAsync(User);
        viewModel.Id = "profile-details";
        viewModel.BasicInfo.ErrorMessage = message;

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        var result = await _profileManager.UploadImage(User, file);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Security(string message = "")
    {
        var viewModel = await _profileManager.PopulateProfileIndexAsync(User);
        viewModel.Id = "profile-security";
        viewModel.Security.ErrorMessage = message;

        return View("Index", viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> SavedCourses()
    {
        var viewModel = await _profileManager.PopulateProfileIndexAsync(User);
        viewModel.Id = "profile-saved-courses";

        return View("Index", await _profileManager.GetSavedCourses(viewModel, User));
    }

    [HttpPost]
    public async Task<IActionResult> BasicInfo(ProfileBasicInfoModel model)
    {
        if (TryValidateModel(model))
        {
            var result = await _profileManager.UpdateBasicInfo(User, model);

            if (result)
            {
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
            var result = await _profileManager.UpdateAddressInfo(User, model);

            if (result)
            {
                return RedirectToAction("Index", new { message = "Success!" });
            }
        }

        return RedirectToAction("Index", new { message = "Invalid information, please try again" });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ProfileSecurityModel model)
    {
        if (TryValidateModel(model))
        {
            var result = await _profileManager.ChangePassword(User, model);

            if (result)
            {
                return RedirectToAction("Security", new { message = "Success!" });
            }
        }

        return RedirectToAction("Security", new { message = "Invalid information, please try again" });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(ProfileSecurityModel model)
    {
        var result = await _profileManager.DeleteAccount(User, model);

        if (result)
        {
            return RedirectToAction("SignIn", "Account");
        }

        return RedirectToAction("Security", new { message = "Invalid information, please try again" });
    }

    public async Task<IActionResult> RemoveCourse(int courseId)
    {
        await _profileManager.RemoveCourse(User, courseId);

        return RedirectToAction("SavedCourses", "Profile");
    }

    public async Task<IActionResult> RemoveAllCourses()
    {
        await _profileManager.RemoveAllCourses(User);

        return RedirectToAction("SavedCourses", "Profile");
    }
}
