using ASPNetMVC.Models.Components;
using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Sections;

namespace ASPNetMVC.Models.Views;

public class HomeIndexViewModel
{
    public string Title { get; set; } = "Ultimate Task Management";
    public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
    {
        Id = "showcase",
        Title = "Task Management Assistant You Gonna Love",
        Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
        Link = new LinkViewModel
        {
            ControllerName = "Account",
            ActionName = "SignUp",
            Text = "Get started for free",
        },
        Brands = new List<ImageViewModel>
        {
            new ImageViewModel {ImageUrl = "images/brand_1.svg", Alt = "company logotype"},
            new ImageViewModel {ImageUrl = "images/brand_2.svg", Alt = "company logotype"},
            new ImageViewModel {ImageUrl = "images/brand_3.svg", Alt = "company logotype"},
            new ImageViewModel {ImageUrl = "images/brand_4.svg", Alt = "company logotype"}
        }
    };

    public FeaturesViewModel Features { get; set; } = new FeaturesViewModel
    {
        Id = "features",
        Title = "What Do You Get With Our Tool?",
        Text = "Make sure all your tasks are organized so you can set the priorities and focus on important.",
        Features = new List<FeatureViewModel>
        {
            new FeatureViewModel
            {
                Image = new ImageViewModel {ImageUrl = "images/icons/chat.svg", Alt = "chat bubble icon"},
                Title = "Comments on Tasks",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new FeatureViewModel
            {
                Image = new ImageViewModel {ImageUrl = "images/icons/presentation.svg", Alt = "presentation screen icon"},
                Title = "Tasks Analytics",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new FeatureViewModel
            {
                Image = new ImageViewModel {ImageUrl = "images/icons/add-group.svg", Alt = "add group icon"},
                Title = "Multiple Assignees",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new FeatureViewModel
            {
                Image = new ImageViewModel {ImageUrl = "images/icons/bell.svg", Alt = "bell icon"},
                Title = "Notifications",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new FeatureViewModel
            {
                Image = new ImageViewModel {ImageUrl = "images/icons/tests.svg", Alt = "check list icon"},
                Title = "Data Security",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            },
            new FeatureViewModel
            {
                Image = new ImageViewModel {ImageUrl = "images/icons/security.svg", Alt = "security shield icon"},
                Title = "Comments on Tasks",
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo."
            }
        }
    };

    public ColourModeViewModel ColourMode { get; set; } = new ColourModeViewModel
    {
        Id = "colour-mode",
        LightTitle = "Switch Between",
        DarkTitle = "Light & Dark Mode",
        Image = new ImageViewModel { ImageUrl = "images/mac.svg", Alt = "mac/laptop. Half light mode, half dark mode" }
    };

    public ManageYourWorkViewModel ManageYourWork { get; set; } = new ManageYourWorkViewModel
    {
        Id = "manage-your-work",
        Title = "Manage Your Work",
        Image = new ImageViewModel { ImageUrl = "images/dashboard.svg", Alt = "The dashboard interface" },
        ListItems = new List<string>{ "Powerful project management", "Transparent work management", "Mange work & focus on the most important tasks", "Track your progress with interactive charts", "Easiest way to track time spent on tasks" }
    };

    public DownloadViewModel Download { get; set; } = new DownloadViewModel
    {
        Id = "download",
        Title = "Download Our App for Any Device",
        Image = new ImageViewModel { ImageUrl = "images/mobile.svg", Alt = "Smartphone" },
        Reviews = new List<ReviewViewModel>
        {
            new ReviewViewModel 
            { 
                Name = "App Store", 
                Title = "Editor's Choice", 
                Rating = "rating 4.7, 187K+ reviews", 
                Image = new ImageViewModel { ImageUrl = "images/appstore.svg", Alt = "app store logo" } 
            },
            new ReviewViewModel
            {
                Name = "Google Play",
                Title = "App of the Day",
                Rating = "rating 4.8, 30K+ reviews",
                Image = new ImageViewModel { ImageUrl = "images/googleplay.svg", Alt = "google play logo" }
            }
        }
    };

    public IntegrationViewModel Integration { get; set; } = new IntegrationViewModel
    {
        Id = "integration",
        Title = "Integrate Top Work Tools",
        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat mollis egestas. Nam luctus facilisis ultrices. Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus.",
        Tools = new List<ToolViewModel>
        {
            new ToolViewModel 
            { 
                Text = "Lorem magnis pretium sed curabitur nunc facilisi nunc cursus sagittis.",
                Image = new ImageViewModel { ImageUrl = "images/icons/google.svg", Alt = "google logo"}
            },
            new ToolViewModel
            {
                Text = "In eget a mauris quis. Tortor dui tempus quis integer est sit natoque placerat dolor.",
                Image = new ImageViewModel { ImageUrl = "images/icons/zoom.svg", Alt = "zoom logo"}
            },
            new ToolViewModel
            {
                Text = "Id mollis consectetur congue egestas egestas suspendisse blandit justo.",
                Image = new ImageViewModel { ImageUrl = "images/icons/slack.svg", Alt = "slack logo"}
            },
            new ToolViewModel
            {
                Text = "Rutrum interdum tortor, sed at nulla. A cursus bibendum elit purus cras praesent.",
                Image = new ImageViewModel { ImageUrl = "images/icons/gmail.svg", Alt = "gmail logo"}
            },
            new ToolViewModel
            {
                Text = "Congue pellentesque amet, viverra curabitur quam diam scelerisque fermentum urna.",
                Image = new ImageViewModel { ImageUrl = "images/icons/trello.svg", Alt = "trello logo"}
            },
            new ToolViewModel
            {
                Text = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris.",
                Image = new ImageViewModel { ImageUrl = "images/icons/mailchimp.svg", Alt = "mailchimp logo"}
            },
            new ToolViewModel
            {
                Text = "Ut in turpis consequat odio diam lectus elementum. Est faucibus blandit platea.",
                Image = new ImageViewModel { ImageUrl = "images/icons/dropbox.svg", Alt = "dropbox logo"}
            },
            new ToolViewModel
            {
                Text = "Faucibus cursus maecenas lorem cursus nibh. Sociis sit risus id. Sit facilisis dolor arcu.",
                Image = new ImageViewModel { ImageUrl = "images/icons/evernote.svg", Alt = "evernote logo"}
            }
        }
    };

    public NewsletterViewModel Newsletter { get; set; } = new NewsletterViewModel();
}
