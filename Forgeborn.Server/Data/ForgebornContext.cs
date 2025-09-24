using Forgeborn.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
    public class ForgebornContext : DbContext
    {
        public ForgebornContext(DbContextOptions<ForgebornContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
