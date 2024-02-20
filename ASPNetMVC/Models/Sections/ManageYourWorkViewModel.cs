using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class ManageYourWorkViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public List<string> ListItems { get; set; } = null!;
}
