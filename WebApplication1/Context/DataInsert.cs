using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Context;

public static class DataInsert
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new DBContext(
                   serviceProvider.GetRequiredService<DbContextOptions<global::WebApplication1.Context.DBContext>>()))
        {
                
            if (context.Characters.Any())
            {
                return;   
            }

            context.Characters.AddRange(
                new Character
                {
                    Id = 10,
                    FirstName = "Bobby",
                    LastName = "Yakuza",
                    CurrentWeight = 50,
                    MaxWeight = 200
                },
                new Character
                {
                    Id = 11,
                    FirstName = "Jane",
                    LastName = "Bobo",
                    CurrentWeight = 30,
                    MaxWeight = 150
                }
            );

            context.Items.AddRange(
                new Item
                {
                    Id = 10,
                    Name = "Item10",
                    Weight = 10
                },
                new Item
                {
                    Id = 20,
                    Name = "Item20",
                    Weight = 11
                },
                new Item
                {
                    Id = 30,
                    Name = "Item30",
                    Weight = 12
                },
                new Item
                {
                    Id = 40,
                    Name = "Item40",
                    Weight = 20
                },
                new Item
                {
                    Id = 50,
                    Name = "Item50",
                    Weight = 15
                }
            );

            context.Titles.AddRange(
                new Title
                {
                    Id = 10,
                    Name = "Title10"
                },
                new Title
                {
                    Id = 20,
                    Name = "Title20"
                },
                new Title
                {
                    Id = 30,
                    Name = "Title30"
                }
            );

            context.CharacterTitles.AddRange(
                new CharacterTitle
                {
                    CharacterId = 10,
                    TitleId = 10,
                    AcquiredAt = DateTime.Parse("2024-06-10T00:00:00")
                },
                new CharacterTitle
                {
                    CharacterId = 10,
                    TitleId = 20,
                    AcquiredAt = DateTime.Parse("2024-06-09T00:00:00")
                },
                new CharacterTitle
                {
                    CharacterId = 10,
                    TitleId = 30,
                    AcquiredAt = DateTime.Parse("2024-06-08T00:00:00")
                },
                new CharacterTitle
                {
                    CharacterId = 20,
                    TitleId = 10,
                    AcquiredAt = DateTime.Parse("2024-06-11T00:00:00")
                }
            );

            context.Backpacks.AddRange(
                new Backpack
                {
                    CharacterId = 10,
                    ItemId = 10,
                    Amount = 20
                },
                new Backpack
                {
                    CharacterId = 10,
                    ItemId = 20,
                    Amount = 10
                },
                new Backpack
                {
                    CharacterId = 20,
                    ItemId = 30,
                    Amount = 30
                }
            );

            context.SaveChanges();
        }
    }
}