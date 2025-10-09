using Microsoft.EntityFrameworkCore;
//using Forgeborn.Server.Models;
using Project_Hexblade.Server.Models;

namespace Forgeborn.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
