using ASPNetMVC.Models.Components;

namespace ASPNetMVC.Models.Sections;

public class SignUpViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public LinkViewModel Link { get; set; } = null!;
}
