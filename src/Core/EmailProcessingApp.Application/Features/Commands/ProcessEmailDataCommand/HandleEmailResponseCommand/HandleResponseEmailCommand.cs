using EmailProcessingApp.Application.Dto;
using MediatR;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.HandleEmailResponseCommand
{
    public class HandleResponseEmailCommand : IRequest<HandleResponseEmailCommandResponse>
    {
        public EmailDataDto EmailDataDto { get; }

        public HandleResponseEmailCommand(EmailDataDto emailDataDto)
        {
            EmailDataDto = emailDataDto;
        }
    }
}
