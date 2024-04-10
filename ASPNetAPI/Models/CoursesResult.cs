namespace ASPNetAPI.Models;

public class CoursesResult
{
    public List<CourseModel> Courses { get; set; } = [];
    public int TotalCourses { get; set; }
}
