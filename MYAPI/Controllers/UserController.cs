using Microsoft.AspNetCore.Mvc;
using MYAPI.Models.EXAPIDTOs;
using MYAPI.Services;

namespace MYAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<ActionResult<UserDto?>> Get(string email, string username, string gender)
            => ExecutionBoundaries(() 
            => _service.Get(email, username, gender));
    }
}
