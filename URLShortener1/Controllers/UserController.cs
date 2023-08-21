using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener1.Data;
using URLShortener1.Entities;

namespace URLShortener1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserController : ControllerBase
    {

        private readonly DataContext _ctx;

        public UserController(DataContext ctx)
        {
            _ctx = ctx;
        }


        [Authorize(Roles = "admin")]
        [HttpPut("updateRole")]
        public async Task<ActionResult<User>> UpdateRole(int userId, string newRole)
        {
            var user = _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId).Result;
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.Role = newRole;


            await _ctx.SaveChangesAsync();


            return Ok(user);

        }

    }
}
