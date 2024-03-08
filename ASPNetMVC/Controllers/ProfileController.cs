using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Views;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetMVC.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly AddressService _addressManager;

    public ProfileController(UserManager<UserEntity> userManager, AddressService addressManager)
    {
        _userManager = userManager;
        _addressManager = addressManager;
    }

    public async Task<IActionResult> Index() 
    {
        return View(await PopulateProfileIndexAsync()); 
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
            }
        }

        return View("Index", await PopulateProfileIndexAsync());
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
                }
            }
        }

        return View("Index", await PopulateProfileIndexAsync());
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
