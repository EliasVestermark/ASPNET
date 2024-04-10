namespace ASPNetMVC.Models.Models;

public class CourseResponse
{
    public List<SingleCourseModel> Courses { get; set; } = [];
    public int TotalCourses { get; set; }
}
