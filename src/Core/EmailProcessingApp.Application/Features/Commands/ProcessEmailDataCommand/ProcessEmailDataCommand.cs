using EmailProcessingApp.Application.Dto;
using MediatR;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand
{
    /// <summary>
    /// Entry command for email processing event. Sent from controller endpoint.
    /// </summary>
    public class ProcessEmailDataCommand : IRequest<ProcessEmailDataCommandResponse>
    {
        public EmailDataDto EmailDataDto { get; }

        public ProcessEmailDataCommand(EmailDataDto clientPayloadDto)
        {
            EmailDataDto = clientPayloadDto;
        }
    }
}
