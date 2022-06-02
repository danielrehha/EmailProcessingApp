using EmailProcessingApp.API.Helpers.Contracts;
using EmailProcessingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailProcessingApp.API.Helpers
{
    public class ResponseManager : ControllerBase, IResponseManager
    {
        public ActionResult<T> MapActionResult<T>(T response, bool noContent = false) where T : ClientCommandResponse
        {
            if (!response.Success)
            {
                return StatusCode((int)response.HttpStatusCode, response);
            }
            if (noContent)
            {
                return Ok();
            }
            return Ok(response);
        }
    }
}
