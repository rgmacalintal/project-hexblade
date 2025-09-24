//using DemoApp.Data.Service;
using Forgeborn.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forgeborn.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public IEnumerable<Users> Get()
        {
            return Enumerable.Range(1, 10).Select(index => new Users { }).ToArray();
        }

    }
}
