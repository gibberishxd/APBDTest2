namespace WebApplication1.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Weight { get; set; }

    public ICollection<Backpack> Backpacks = new List<Backpack>();
}
