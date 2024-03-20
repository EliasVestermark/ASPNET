namespace ASPNetMVC.Models.Models;

public class Course
{
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? BestSeller { get; set; }
    public string Author { get; set; } = null!;
    public string NewPrice { get; set; } = null!;
    public string? OldPrice { get; set; }
    public string? Sale { get; set; }
    public string Duration { get; set; } = null!;
    public string RatingPercent { get; set; } = null!;
    public string RatingLikes { get; set; } = null!;
}
