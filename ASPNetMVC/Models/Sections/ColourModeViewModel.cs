using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class ColourModeViewModel
{
    public string Id { get; set; } = null!;
    public string LightTitle { get; set; } = null!;
    public string DarkTitle { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
}
