using Forgeborn.Server.Models;

namespace Forgeborn.Server.Data.Service
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAll();
        Task Add(Users user);
    }
}
