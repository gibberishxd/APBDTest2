namespace WebApplication1.DTO;

public class CharacterDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemDTO> ItemsBackpack { get; set; }
    public List<CharacterTitleDTO> Titles { get; set; }
}