using Forgeborn.Server.Data.Service;
using Forgeborn.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forgeborn.Server.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            //var users = await _userService.GetAll();
            //return View(users);
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Add(user);

                return RedirectToAction("Index");
            }
            return View();
            //return null;
        }

        [HttpGet]
        public async Task<IActionResult> Get(User user)
        {
            var users = await _userService.GetAll();
            return View(users);
        }
    }
}
