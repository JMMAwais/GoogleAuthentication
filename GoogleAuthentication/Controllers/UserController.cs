using GoogleAuthentication.Data;
using GoogleAuthentication.Models;
using GoogleAuthentication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoogleAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(ApplicationDbContext db,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> Login([FromBody] LoginVM login)
        {
            var normalizedEmail = _userManager.NormalizeEmail(login.Email);
            var user = _userManager.FindByEmailAsync(normalizedEmail).Result;

            var user1 = await _db.Users.SingleOrDefaultAsync(u => u.Email == "Awais@gmail.com");

            if (user == null)
            {
                return Unauthorized("User not found");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (result.Succeeded)
            {
                // Return user or token as per your requirement
                return Ok(user);
            }

            return Unauthorized("Invalid login attempt");
        }
    

    // GET api/<UserController>/5
    [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
