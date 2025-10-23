using Forgeborn.Server.Models;
using Forgeborn.Server.Models.Items;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Forgeborn.Server.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "forgebornDB",
                new MySqlServerVersion(new Version(8, 4, 6))
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Characters>(entity =>
            {
                // CS1660: Use HasConversion(Func<TModel, TProvider>, Func<TProvider, TModel>)
                entity.Property(e => e.Stats)
                      .HasColumnType("json")
                      .HasConversion(
                          v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                          v => JObject.Parse(v)
                      );
            });
        }

        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Characters> Characters { get; set; } = default!;
        public DbSet<CharacterRulesets> CharacterRulesets { get; set; } = default!;
        public DbSet<Lobbys> Lobbys { get; set; } = default!;
        public DbSet<Players> Players { get; set; } = default!;
        public DbSet<Rulesets> Rulesets { get; set; } = default!;
        
        // Item Subclasses
        public DbSet<AdventuringGears> AdventuringGears { get; set; } = default!;
        public DbSet<Ammunitions> Ammunitions { get; set; } = default!;
        public DbSet<Armors> Armors { get; set; } = default!;
        public DbSet<Mounts> Mounts { get; set; } = default!;
        public DbSet<SpellScrolls> SpellScrolls { get; set; } = default!;
        public DbSet<Tools> Tools { get; set; } = default!;
        public DbSet<Vehicles> Vehicles { get; set; } = default!;
        public DbSet<Weapons> Weapons { get; set; } = default!;

    }
}
