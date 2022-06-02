using EmailProcessingApp.API.Helpers.Contracts;
using EmailProcessingApp.Application.Dto;
using EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmailProcessingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailDataController : ControllerBase
    {
        private readonly IResponseManager _responseManager;
        private readonly IMediator _mediator;

        public EmailDataController(IResponseManager responseManager, IMediator mediator)
        {
            _responseManager = responseManager;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ProcessEmailDataCommandResponse>> ProcessPayloadAsync([FromBody] EmailDataDto dto)
        {
            var result = await _mediator.Send(new ProcessEmailDataCommand(dto));

            return _responseManager.MapActionResult(result, noContent: true);
        }
    }
}
