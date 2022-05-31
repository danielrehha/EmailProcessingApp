using EmailProcessingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailProcessingApp.API.Helpers.Contracts
{
    public interface IResponseManager
    {
        ActionResult<T> MapActionResult<T>(T response) where T : BaseResponse;
    }
}
