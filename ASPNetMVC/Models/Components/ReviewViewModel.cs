namespace ASPNetMVC.Models.Components;

public class ReviewViewModel
{
    public string Name { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Rating { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
}
