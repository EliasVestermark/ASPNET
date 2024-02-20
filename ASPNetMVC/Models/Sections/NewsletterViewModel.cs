using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class NewsletterViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public List<string> Checkboxes { get; set; } = null!;
}
