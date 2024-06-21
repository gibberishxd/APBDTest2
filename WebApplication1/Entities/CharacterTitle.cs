namespace WebApplication1.Entities;

public class CharacterTitle
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    public DateTime AcquiredAt { get; set; }

    public virtual Character Character { get; set; }
    public virtual Title Title { get; set; }
}