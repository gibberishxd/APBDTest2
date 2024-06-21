using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly DBContext _dbContext;

    public CharactersController(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{characterId}")]
    public async Task<ActionResult<CharacterDTO>> GetCharacter(int characterId)
    {
        var chara = await _dbContext.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .Where(c => c.Id == characterId)
            .Select(c => new CharacterDTO
            {
                CurrentWeight = c.CurrentWeight,
                MaxWeight = c.MaxWeight,
                FirstName = c.FirstName,
                LastName = c.LastName,
                ItemsBackpack = c.Backpacks.Select(b => new BackpackItemDTO
                {
                    Amount = b.Amount,
                    NameItem = b.Item.Name,
                    ItemWeight = b.Item.Weight
                }).ToList(),
                Titles = c.CharacterTitles.Select(ct => new CharacterTitleDTO
                {
                    AcquiredAt = ct.AcquiredAt,
                    Title = ct.Title.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (chara == null)
        {
            return NotFound();
        }

        return Ok(chara);
    }
}