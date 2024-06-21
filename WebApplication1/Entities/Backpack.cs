namespace WebApplication1.Entities;

public class Backpack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    public virtual Character Character { get; set; }
    public virtual Item Item { get; set; }
}