namespace ASPNetMVC.Models.Models;

public class CoursesModel
{
    public List<SingleCourseModel> Courses { get; set; } = [];
    public int TotalCourses { get; set; }
    public int Page { get; set; }
}
