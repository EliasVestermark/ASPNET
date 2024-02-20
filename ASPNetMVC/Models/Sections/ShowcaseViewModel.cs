using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class ShowcaseViewModel
{
    public string? Id {  get; set; } 
    public string? Title { get; set; }
    public string? Text { get; set; }
    public LinkViewModel Link { get; set; } = new LinkViewModel();
    public List<ImageViewModel>? Brands { get; set; }
}
