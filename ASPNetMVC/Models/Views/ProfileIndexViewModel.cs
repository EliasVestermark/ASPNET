using ASPNetMVC.Models.Models;
using Infrastructure.Entities;

namespace ASPNetMVC.Models.Views;

public class ProfileIndexViewModel
{
    public string Id { get; set; } = "profile-details";
    public string Title { get; set; } = "Account Details";
    public ProfileBasicInfoModel BasicInfo { get; set; } = new ProfileBasicInfoModel()
    {
        ProfileImage = "images/john.svg"
    };
    public ProfileAddressModel AddressInfo { get; set; } = new ProfileAddressModel();
}
