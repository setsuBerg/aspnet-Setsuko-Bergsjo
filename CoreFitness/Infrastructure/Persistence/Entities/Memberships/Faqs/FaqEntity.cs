namespace Infrastructure.Persistence.Entities.Memberships.Faqs;

public class FaqEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!; 
}
