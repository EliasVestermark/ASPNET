using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<SubscriberEntity> Subscribers { get; set; }
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<WhatYouLearnEntity> WhatYouLearns { get; set; }
    public DbSet<IncludesEntity> Includes { get; set; }
    public DbSet<ProgramDetailsEntity> ProgramDetails { get; set; }
    public DbSet<TagEntity> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Courses)
            .WithMany(c => c.Users)
            .UsingEntity(j => j.ToTable("UserCourses"));

        modelBuilder.Entity<CourseEntity>()
            .HasMany(c => c.WhatYouLearns)
            .WithMany(w => w.Courses)
            .UsingEntity(j => j.ToTable("CourseWhatYouLearn"));

        modelBuilder.Entity<CourseEntity>()
            .HasMany(c => c.Includes)
            .WithMany(i => i.Courses)
            .UsingEntity(j => j.ToTable("CourseIncludes"));

        modelBuilder.Entity<CourseEntity>()
            .HasMany(c => c.ProgramDetails)
            .WithMany(p => p.Courses)
            .UsingEntity(j => j.ToTable("CourseProgramDetails"));

        modelBuilder.Entity<TagEntity>()
            .HasMany(t => t.Courses)
            .WithMany(c => c.Tags)
            .UsingEntity(j => j.ToTable("CourseTags"));

        //modelBuilder.Entity<LabelEntity>()
        //    .HasMany(l => l.Courses)
        //    .WithMany(c => c.Labels)
        //    .UsingEntity(j => j.ToTable("CourseLabels"));
    }
}
