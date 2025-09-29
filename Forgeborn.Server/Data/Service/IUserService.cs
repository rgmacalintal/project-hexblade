using Forgeborn.Server.Models;

namespace Forgeborn.Server.Data.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task Add(User user);
    }
}
