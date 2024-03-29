using ASPNetAPI.Models;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNetAPI.Services;

public class CourseService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<bool> CreateCourse(CourseModel model)
    {
        try
        {
            var tagList = await CreateTag(model);
            var whatYouLearnList = await CreateWhatYouLearn(model);
            var includesList = await CreateIncludes(model);
            var programDetailsList = await CreateProgramDetails(model);

            var courseEntity = new CourseEntity
            {
                CourseTitle = model.CourseTitle,
                Subtitle = model.Subtitle,
                Reviews = model.Reviews,
                Likes = model.Likes,
                Duration = model.Duration,
                Author = model.Author,
                Description = model.Description,
                NewPrice = model.NewPrice,
                OldPrice = model.OldPrice,
                AuthorDescription = model.AuthorDescription,
                Subscribers = model.Subscribers,
                Followers = model.Followers,
                AuthorIcon = model.AuthorIcon,
                AuthorImage = model.AuthorImage,
                BackgroundImage = model.BackgroundImage,
                LikesPercent = model.LikesPercent,
                Tags = tagList.ToList(),
                WhatYouLearns = whatYouLearnList.ToList(),
                Includes = includesList.ToList(),
                ProgramDetails = programDetailsList.ToList()
            };

            _context.Courses.Add(courseEntity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<CourseModel>> GetAllCourseModels()
    {
        var courses = await _context.Courses
            .Include(course => course.Tags)
            .Include(course => course.WhatYouLearns)
            .Include(course => course.Includes)
            .Include(course => course.ProgramDetails)
            .ToListAsync();

        if (courses != null)
        {
            var courseModelList = new List<CourseModel>();

            foreach (var course in courses)
            {
                courseModelList.Add(PopulateCourseModel(course));
            }

            return courseModelList;
        }

        return null!;
    }

    public async Task<CourseModel> GetOneCourseModel(int id)
    {
        var course = await _context.Courses
            .Include(course => course.Tags)
            .Include(course => course.WhatYouLearns)
            .Include(course => course.Includes)
            .Include(course => course.ProgramDetails)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {
            return PopulateCourseModel(course!);
        }

        return null!;
    }

    public async Task<CourseEntity> GetOneCourseEntity(int id)
    {
        var course = await _context.Courses
            .Include(course => course.Tags)
            .Include(course => course.WhatYouLearns)
            .Include(course => course.Includes)
            .Include(course => course.ProgramDetails)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (course != null)
        {
            return course;
        }

        return null!;
    }

    public async Task<CourseModel> UpdateCourse(CourseModel model, CourseEntity courseEntity)
    {
        try
        {
            var tagList = await CreateTag(model);
            var whatYouLearnList = await CreateWhatYouLearn(model);
            var includesList = await CreateIncludes(model);
            var programDetailsList = await CreateProgramDetails(model);

            courseEntity.CourseTitle = model.CourseTitle;
            courseEntity.Subtitle = model.Subtitle;
            courseEntity.Reviews = model.Reviews;
            courseEntity.Likes = model.Likes;
            courseEntity.Duration = model.Duration;
            courseEntity.Author = model.Author;
            courseEntity.Description = model.Description;
            courseEntity.NewPrice = model.NewPrice;
            courseEntity.OldPrice = model.OldPrice;
            courseEntity.AuthorDescription = model.AuthorDescription;
            courseEntity.Subscribers = model.Subscribers;
            courseEntity.Followers = model.Followers;
            courseEntity.AuthorIcon = model.AuthorIcon;
            courseEntity.AuthorImage = model.AuthorImage;
            courseEntity.BackgroundImage = model.BackgroundImage;
            courseEntity.LikesPercent = model.LikesPercent;
            courseEntity.Tags = tagList.ToList();
            courseEntity.WhatYouLearns = whatYouLearnList.ToList();
            courseEntity.Includes = includesList.ToList();
            courseEntity.ProgramDetails = programDetailsList.ToList();

            _context.Update(courseEntity);
            await _context.SaveChangesAsync();

            return PopulateCourseModel(courseEntity!);
        }
        catch
        {
            return null!;
        }
    }

    public CourseModel PopulateCourseModel(CourseEntity course)
    {
        var tags = new List<string>();
        var whatYouLearn = new List<string>();
        var includes = new List<string>();
        var programDetails = new List<ProgramDetailsModel>();

        foreach (var item in course.Tags!)
        {
            tags.Add(item.Description.ToString());
        }

        foreach (var item in course.WhatYouLearns)
        {
            whatYouLearn.Add(item.Description.ToString());
        }

        foreach (var item in course.Includes)
        {
            includes.Add(item.Description.ToString());
        }

        foreach (var item in course.ProgramDetails)
        {
            programDetails.Add(new ProgramDetailsModel { DetailTitle = item.Title, DetailDescription = item.Description });
        }

        var courseModel = new CourseModel
        {
            Id = course.Id,
            CourseTitle = course.CourseTitle,
            Tags = tags,
            Subtitle = course.Subtitle,
            Reviews = course.Reviews,
            Likes = course.Likes,
            Duration = course.Duration,
            Author = course.Author,
            Description = course.Description,
            WhatYouLearn = whatYouLearn,
            Includes = includes,
            NewPrice = course.NewPrice,
            OldPrice = course.OldPrice,
            ProgramDetails = programDetails,
            AuthorDescription = course.AuthorDescription,
            Subscribers = course.Subscribers,
            Followers = course.Followers,
            AuthorIcon = course.AuthorIcon,
            AuthorImage = course.AuthorImage,
            BackgroundImage = course.BackgroundImage,
            LikesPercent = course.LikesPercent
        };

        return courseModel;
    }

    public async Task<IEnumerable<TagEntity>> CreateTag(CourseModel model)
    {
        try
        {
            var idList = new List<TagEntity>();

            if (model.Tags != null)
            {
                foreach (var item in model.Tags)
                {
                    if (!await _context.Tags.AnyAsync(x => x.Description == item))
                    {
                        var tagEntity = new TagEntity
                        {
                            Description = item
                        };

                        _context.Tags.Add(tagEntity);
                        await _context.SaveChangesAsync();
                    }

                    var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Description == item);

                    idList.Add(tag!);
                }
            }

            return idList;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<IEnumerable<WhatYouLearnEntity>> CreateWhatYouLearn(CourseModel model)
    {
        try
        {
            var idList = new List<WhatYouLearnEntity>();

            if (model.WhatYouLearn != null)
            {
                foreach (var item in model.WhatYouLearn)
                {
                    if (!await _context.WhatYouLearns.AnyAsync(x => x.Description == item))
                    {
                        var whatYouLearnEntity = new WhatYouLearnEntity
                        {
                            Description = item
                        };

                        _context.WhatYouLearns.Add(whatYouLearnEntity);
                        await _context.SaveChangesAsync();
                    }

                    var whatYouLearn = await _context.WhatYouLearns.FirstOrDefaultAsync(x => x.Description == item);

                    idList.Add(whatYouLearn!);
                }
            }

            return idList;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<IEnumerable<IncludesEntity>> CreateIncludes(CourseModel model)
    {
        try
        {
            var idList = new List<IncludesEntity>();

            if (model.Includes != null)
            {
                foreach (var item in model.Includes)
                {
                    if (!await _context.Includes.AnyAsync(x => x.Description == item))
                    {
                        var includesEntity = new IncludesEntity
                        {
                            Description = item
                        };

                        _context.Includes.Add(includesEntity);
                        await _context.SaveChangesAsync();
                    }

                    var includes = await _context.Includes.FirstOrDefaultAsync(x => x.Description == item);

                    idList.Add(includes!);
                }
            }

            return idList;
        }
        catch
        {
            return null!;
        }
    }

    public async Task<IEnumerable<ProgramDetailsEntity>> CreateProgramDetails(CourseModel model)
    {
        try
        {
            var idList = new List<ProgramDetailsEntity>();

            if (model.ProgramDetails != null)
            {
                foreach (var item in model.ProgramDetails)
                {
                    if (!await _context.ProgramDetails.AnyAsync(x => x.Description == item.DetailDescription))
                    {
                        var programDetailsEntity = new ProgramDetailsEntity
                        {
                            Title = item.DetailTitle,
                            Description = item.DetailDescription
                        };

                        _context.ProgramDetails.Add(programDetailsEntity);
                        await _context.SaveChangesAsync();
                    }

                    var programDetail = await _context.ProgramDetails.FirstOrDefaultAsync(x => x.Description == item.DetailDescription);

                    idList.Add(programDetail!);
                }
            }

            return idList;
        }
        catch
        {
            return null!;
        }
    }
}
