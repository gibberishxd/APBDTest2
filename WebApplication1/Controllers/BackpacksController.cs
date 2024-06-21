using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BackpacksController : ControllerBase
{
    private readonly DBContext _context;

        public BackpacksController(DBContext context)
        {
            _context = context;
        }

        [HttpPost("{characterId}/backpacks")]
        public async Task<ActionResult<IEnumerable<Backpack>>> AddItemsToBackpack(int characterId, List<AddItemToBackpackDTO> addItems)
        {
            var chara = await _context.Characters
                .Include(c => c.Backpacks)
                .FirstOrDefaultAsync(c => c.Id == characterId);

            if (chara == null)
            {
                return NotFound();
            }

            var itemsIds = addItems.Select(i => i.ItemId).ToList();
            var items = await _context.Items.Where(i => itemsIds.Contains(i.Id)).ToListAsync();

            if (items.Count != itemsIds.Count)
            {
                return BadRequest("Does not exist.");
            }

            var weightToAdd = addItems
                .Join(items, it => it.ItemId, i => i.Id, (it, i) => new { it.Amount, i.Weight })
                .Sum(x => x.Amount * x.Weight);

            if (chara.CurrentWeight + weightToAdd > chara.MaxWeight)
            {
                return BadRequest("Not enough free weight.");
            }

            foreach (var itemToAdd in addItems)
            {
                var backpackItem = chara.Backpacks
                    .FirstOrDefault(b => b.ItemId == itemToAdd.ItemId);

                if (backpackItem == null)
                {
                    chara.Backpacks.Add(new Backpack
                    {
                        CharacterId = characterId,
                        ItemId = itemToAdd.ItemId,
                        Amount = itemToAdd.Amount
                    });
                }
                else
                {
                    backpackItem.Amount += itemToAdd.Amount;
                }
            }

            chara.CurrentWeight += weightToAdd;

            await _context.SaveChangesAsync();

            var result = chara.Backpacks.Select(b => new
            {
                b.Amount,
                b.ItemId,
                b.CharacterId
            });

            return Ok(result);
        }
}