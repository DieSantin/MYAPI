using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYAPI.Services.EXAPI;

namespace MYAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EXAPIController : ControllerBase
    {
        private readonly IEXAPIService _exApi;

        public EXAPIController(IEXAPIService exApi)
        {
            _exApi = exApi;
        }

        [HttpGet("get-user-from-info/email={email}/username={username}/gender={gender}")]
        public async Task<IActionResult> GetUserFromInfo(string email, string username, string gender)
        {
            var data = await _exApi.GetUserFromInfo(email, username, gender);
            
            if (data == null)
            {
                return NoContent();
            }
            else if (data == null && EXAPIService.EXAPINotFound)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
