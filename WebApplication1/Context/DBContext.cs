using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Context;

public class DBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        =>optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Character>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Title>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Backpack>()
            .HasKey(b => new { b.CharacterId, b.ItemId });

        modelBuilder.Entity<Backpack>()
            .HasOne(b => b.Character)
            .WithMany(c => c.Backpacks)
            .HasForeignKey(b => b.CharacterId);

        modelBuilder.Entity<Backpack>()
            .HasOne(b => b.Item)
            .WithMany(i => i.Backpacks)
            .HasForeignKey(b => b.ItemId);

        modelBuilder.Entity<CharacterTitle>()
            .HasKey(ct => new { ct.CharacterId, ct.TitleId });

        modelBuilder.Entity<CharacterTitle>()
            .HasOne(ct => ct.Character)
            .WithMany(c => c.CharacterTitles)
            .HasForeignKey(ct => ct.CharacterId);

        modelBuilder.Entity<CharacterTitle>()
            .HasOne(ct => ct.Title)
            .WithMany(t => t.CharacterTitles)
            .HasForeignKey(ct => ct.TitleId);
    }
}