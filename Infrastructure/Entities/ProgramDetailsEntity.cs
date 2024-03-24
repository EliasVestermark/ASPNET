namespace Infrastructure.Entities
{
    public class ProgramDetailsEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<CourseEntity> Courses { get; set; } = null!;
    }
}
