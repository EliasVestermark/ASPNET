namespace ASPNetAPI.Models;

public class CourseModel
{
    public int Id { get; set; }
    public string CourseTitle { get; set; } = null!;
    public List<string> Tags { get; set; } = [];
    public string Subtitle { get; set; } = null!;
    public string Reviews { get; set; } = null!;
    public string Likes { get; set; } = null!;
    public string Duration { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> WhatYouLearn { get; set; } = [];
    public List<string> Includes { get; set; } = [];
    public string NewPrice { get; set; } = null!;
    public string OldPrice { get; set; } = null!;
    public List<ProgramDetailsModel> ProgramDetails { get; set; } = [];
    public string AuthorDescription { get; set; } = null!;
    public string Subscribers { get; set; } = null!;
    public string Followers { get; set; } = null!;
    public string AuthorIcon { get; set; } = null!;
    public string AuthorImage { get; set; } = null!;
    public string BackgroundImage { get; set; } = null!;
    public string LikesPercent { get; set; } = null!;
}

