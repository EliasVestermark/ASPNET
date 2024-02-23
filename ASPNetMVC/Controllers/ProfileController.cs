using ASPNetMVC.Models.Views;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetMVC.Controllers;

public class ProfileController : Controller
{
    //private readonly ProfileService _profileService;

    //public ProfileController(ProfileService profileService)
    //{
    //    _profileService = profileService;
    //}

    public IActionResult Index() 
    { 
        var viewModel = new ProfileIndexViewModel();

        //viewModel.BasicInfo = _profileService.GetBasicInfo();
        //viewModel.AddressInfo = _profileService.GetAddressInfo();

        return View(viewModel); 
    }

    [HttpPost]
    public IActionResult BasicInfo(ProfileIndexViewModel viewModel)
    {
        //_profileService.SaveBasicInfo(viewModel.BasicInfo);

        return RedirectToAction(nameof(Index), viewModel);
    }

    [HttpPost]
    public IActionResult AddressInfo(ProfileIndexViewModel viewModel)
    {
        //_profileService.SaveAddressInfo(viewModel.AddressInfo);

        return RedirectToAction(nameof(Index), viewModel);
    }
}
