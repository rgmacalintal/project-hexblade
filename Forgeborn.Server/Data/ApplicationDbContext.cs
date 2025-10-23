using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using Forgeborn.Server.Models;
using Forgeborn.Server.Models.Items;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Forgeborn.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Armors>(x => {
                x.ComplexProperty(y => y.Traits, y => { y.IsRequired(); });
            });

            modelBuilder.Entity<Weapons>(x => {
                x.ComplexProperty(y => y.DamageType, y => { y.IsRequired(); });
                x.ComplexProperty(y => y.Traits, y => { y.IsRequired(); });
            });

            modelBuilder.Entity<Characters>(entity =>
            {
                entity.Property(e => e.Inventory)
                .HasColumnType("json")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<JObject>(v, new JsonSerializerOptions())
                    );
            });

            base.OnModelCreating(modelBuilder);
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
