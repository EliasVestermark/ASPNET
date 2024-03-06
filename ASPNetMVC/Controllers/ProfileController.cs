using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Views;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<UserEntity> _userManager;

    public ProfileController(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> Index() 
    {
        var userEntity = await _userManager.GetUserAsync(User);

        var viewModel = new ProfileIndexViewModel
        {
            User = userEntity!
        };

        return View(viewModel); 
    }

    [HttpPost]
    public IActionResult BasicInfo(ProfileIndexViewModel viewModel)
    {
        if (TryValidateModel(viewModel.BasicInfo))
        {
            return RedirectToAction("Index", "Profile", viewModel);
        }

        return RedirectToAction("Index", "Profile", viewModel);
    }

    [HttpPost]
    public IActionResult AddressInfo(ProfileIndexViewModel viewModel)
    {
        if (TryValidateModel(viewModel.AddressInfo))
        {
            return RedirectToAction("Index", "Profile", viewModel);
        }

        return RedirectToAction("Index", "Profile", viewModel);
    }

    //private async Task<ProfileBasicInfoModel> PopulateProfileBasicInfoAsync()
    //{
    //    var user = await _userManager.GetUserAsync(User);
    //    if (user != null)
    //    {
    //        return new ProfileBasicInfoModel
    //        {
    //            UserId = user.Id,
    //            FirstName = user.FirstName,
    //            LastName = user.LastName,
    //            Email = user.Email!,
    //            Phone = user.PhoneNumber,

    //        };
    //    }
    //}
}
