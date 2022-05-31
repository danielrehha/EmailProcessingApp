using EmailProcessingApp.Application.Dto;
using MediatR;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand
{
    public class ProcessEmailDataCommand : IRequest<ProcessEmailDataCommandResponse>
    {
        public EmailDataDto EmailDataDto { get; }

        public ProcessEmailDataCommand(EmailDataDto clientPayloadDto)
        {
            EmailDataDto = clientPayloadDto;
        }
    }
}
