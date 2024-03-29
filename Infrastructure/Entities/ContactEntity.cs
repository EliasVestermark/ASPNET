namespace Infrastructure.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string? Service { get; set; }
        public string Message { get; set; } = null!;
    }
}
