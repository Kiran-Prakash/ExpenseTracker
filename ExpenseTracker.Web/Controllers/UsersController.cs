using ExpenseTracker.Core.Entities;
using ExpenseTracker.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            if (userId < 0)
            {
                return BadRequest("User ID must be greater than zero.");
            }

            var user = await _userService.GetUserByID(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            var createdUser = await _userService.AddUser(user);
            return CreatedAtAction(nameof(AddUser), new { userId = createdUser.UserId }, createdUser);
        }

        [HttpPost("/family")]
        public async Task<IActionResult> AddFamily([FromBody] Family family)
        {
            if(family == null)
            { 
                return BadRequest("Family cannot be null.");
            }
            var createdFamily = await _userService.AddFamily(family);
            return CreatedAtAction(nameof(AddFamily), new { familyId = createdFamily.FamilyId }, createdFamily);
        }

        [HttpDelete("/family/{familyId:int}")]
        public async Task<IActionResult> DeleteFamily(int familyId)
        {
            await _userService.DeleteFamily(familyId);
            return NoContent();
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("/family/{familyId:int}")]
        public async Task<IActionResult> GetFamilyById(int familyId)
        {
            if (familyId < 0)
            {
                return BadRequest("Family ID must be greater than zero.");
            }

            var family = await _userService.GetFamilyByID(familyId);
            if (family == null)
            {
                return NotFound();
            }

            return Ok(family);
        }

        [HttpGet("/family/users/{familyId:int}")]
        public async Task<IActionResult> GetUsersByFamilyId(int familyId)
        {
            if (familyId < 0)
            {
                return BadRequest("Family ID must be greater than zero.");
            }

            var users = await _userService.GetUsersByFamilyID(familyId);
            if (users == null || !users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User cannot be null.");
            }

            var updatedUser = await _userService.UpdateUser(user);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpPut("/family")]
        public async Task<IActionResult> UpdateFamily([FromBody] Family family)
        {
            if (family == null)
            {
                return BadRequest("Family cannot be null.");
            }

            var updatedFamily = await _userService.UpdateFamily(family);
            if (updatedFamily == null)
            {
                return NotFound();
            }

            return Ok(updatedFamily);
        }
    }
}
