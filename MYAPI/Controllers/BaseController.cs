using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYAPI.Common.Exceptions;
using MYAPI.Services;

namespace MYAPI.Controllers
{
    public class BaseController : ControllerBase
    {

        public BaseController()
        {
        }

        protected async Task<ActionResult<T>> ExecutionBoundaries<T>(Func<Task<T>> toExecute)
        {
            try
            {
                var result = await toExecute();
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "OMG Something's wrong");
            }
        }
    }
}
