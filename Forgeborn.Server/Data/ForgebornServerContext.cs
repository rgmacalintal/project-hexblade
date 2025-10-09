using Forgeborn.Server.Models;
using Forgeborn.Server.Models.Items;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Forgeborn.Server.Data
{
    public class ForgebornServerContext : DbContext
    {
        public ForgebornServerContext (DbContextOptions<ForgebornServerContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;

    }
}
