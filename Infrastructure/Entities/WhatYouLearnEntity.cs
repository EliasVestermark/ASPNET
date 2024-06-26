﻿namespace Infrastructure.Entities
{
    public class WhatYouLearnEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public ICollection<CourseEntity> Courses { get; set; } = null!;
    }
}
