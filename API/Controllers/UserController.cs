using ApplicationService.Users;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            return HandleResult(await Mediator.Send(new Create.Command(user)));
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}
