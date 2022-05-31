using EmailProcessingApp.API.Helpers.Contracts;
using EmailProcessingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailProcessingApp.API.Helpers
{
    public class ResponseManager : ControllerBase, IResponseManager
    {
        public ActionResult<T> MapActionResult<T>(T response) where T : BaseResponse
        {
            if (!response.Success)
            {
                return StatusCode((int)response.HttpStatusCode, response);
            }
            return Ok(response);
        }
    }
}
