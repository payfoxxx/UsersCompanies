using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IEnumerable<User>? GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpPost]
        public User AddNewUser([FromBody] User user)
        {
            return _userService.CreateUser(user);
        }

        [HttpPut]
        public IActionResult EditUser([FromBody] User user)
        {
            _userService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveUser([FromRoute] int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
