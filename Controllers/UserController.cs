using GamesApi.Domain.Models;
using GamesApi.Domain.Services;
using GamesApi.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            //This method is basically login authentication.
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user.Token);
        }
        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            var users = _userService.GetAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}