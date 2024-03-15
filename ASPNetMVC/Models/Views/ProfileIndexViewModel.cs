using ASPNetMVC.Models.Models;

namespace ASPNetMVC.Models.Views;

public class ProfileIndexViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = "Account Details";
    public bool IsExternalAccount { get; set; }
    public ProfileBasicInfoModel BasicInfo { get; set; } = new ProfileBasicInfoModel();
    public ProfileAddressModel AddressInfo { get; set; } = new ProfileAddressModel();
    public ProfileSecurityModel Security {  get; set; } = new ProfileSecurityModel();
    public ProfileSavedCoursesModel SavedCoursesModel {  get; set; } = new ProfileSavedCoursesModel();
}
