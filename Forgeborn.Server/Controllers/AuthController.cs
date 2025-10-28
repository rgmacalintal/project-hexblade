using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forgeborn.Server.Models;
using Forgeborn.Server.Data;
using System.Security.Cryptography;
using System.Text;

namespace Forgeborn.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class LoginRequest
        {
            public string Username { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return Conflict("Email already exists.");
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                return Conflict("Username already exists.");

            user.Password = HashPassword(user.Password);
            user.CreatedOn = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null)
                return Unauthorized("Invalid username or password.");


            if (!VerifyPassword(request.Password, user.Password)) 
                return Unauthorized("Invalid username or password.");

            return Ok(new
            {
                message = "Login successful.",
                username = user.Username,
                email = user.Email
            });
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
        private bool VerifyPassword(string entered, string stored)
        {
            return HashPassword(entered) == stored;
        }
    }
}
