using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class FeaturesViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public List<FeatureViewModel> Features { get; set; } = null!;
}
