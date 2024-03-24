namespace Infrastructure.Entities
{
    public class LabelEntity
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public ICollection<CourseEntity>? Courses { get; set; }
    }
}
