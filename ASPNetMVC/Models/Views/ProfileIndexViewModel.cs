using ASPNetMVC.Controllers;
using ASPNetMVC.Models.Models;

namespace ASPNetMVC.Models.Views;

public class ProfileIndexViewModel
{
    public string Id { get; set; } = "profile-details";
    public string Title { get; set; } = "Account Details";

    public ProfileBasicInfoModel BasicInfo { get; set; } = new ProfileBasicInfoModel()
    {
        ProfileImage = "images/john.svg",
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@domain.com"
    };
    public ProfileAddressModel AddressInfo {  get; set; } = new ProfileAddressModel();
}
