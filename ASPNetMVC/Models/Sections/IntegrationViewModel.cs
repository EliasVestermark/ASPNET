using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class IntegrationViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public List<ToolViewModel> Tools { get; set; } = null!;
}
