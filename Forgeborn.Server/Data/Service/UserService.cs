using Forgeborn.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Forgeborn.Server.Data.Service
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
