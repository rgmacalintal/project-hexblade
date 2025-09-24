using Forgeborn.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Forgeborn.Server.Data.Service
{
    public class UserService : IUserService
    {
        private readonly ForgebornContext _context;

        public UserService(ForgebornContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}
