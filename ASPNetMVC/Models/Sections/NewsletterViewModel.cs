using ASPNetMVC.Models.Components;
using ASPNetMVC.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Sections;

public class NewsletterViewModel
{
    public string Id { get; set; } = "newsletter";
    public string Title { get; set; } = "Don't Want To Miss Anything?";
    public string Text { get; set; } = "Sign up for Newsletters";
    public ImageViewModel Image { get; set; } = new ImageViewModel { ImageUrl = "images/icons/curlyarrow.svg", Alt = "curly arrow" };
    public SubscribeModel SubscribeModel { get; set; } = new SubscribeModel();
}
