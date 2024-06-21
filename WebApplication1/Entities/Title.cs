namespace WebApplication1.Entities;

public class Title
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<CharacterTitle> CharacterTitles = new List<CharacterTitle>();
}

