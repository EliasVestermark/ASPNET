namespace Infrastructure.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; } = null!;
        public string Subtitle { get; set; } = null!;
        public string Reviews { get; set; } = null!;
        public string Likes { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string NewPrice { get; set; } = null!;
        public string OldPrice { get; set; } = null!;
        public string AuthorDescription { get; set; } = null!;
        public string Subscribers { get; set; } = null!;
        public string Followers { get; set; } = null!;
        public string AuthorIcon { get; set; } = null!;
        public string AuthorImage { get; set; } = null!;

        public ICollection<UserEntity>? Users { get; set; }
        public ICollection<TagEntity>? Tags { get; set; } = null!;
        public ICollection<LabelEntity>? Labels { get; set; } = null!;
        public ICollection<WhatYouLearnEntity> WhatYouLearns { get; set; } = null!;
        public ICollection<IncludesEntity> Includes { get; set; } = null!;
        public ICollection<ProgramDetailsEntity> ProgramDetails { get; set; } = null!;
    }
}
