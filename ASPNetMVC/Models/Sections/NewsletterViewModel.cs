using ASPNetMVC.Models.Components;
using System.ComponentModel.DataAnnotations;

namespace ASPNetMVC.Models.Sections;

public class NewsletterViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public ImageViewModel Image { get; set; } = null!;
    public List<string> Checkboxes { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}
