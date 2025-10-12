using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Project_Hexblade.Server.Models;
using System.Text.Json;

namespace Forgeborn.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        //public DbSet<LogEntry> Logs => Set<LogEntry>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<Rulesets> Rulesets { get; set; }
        public DbSet<CharacterRulesets> CharacterRules { get; set; }
    }
}
