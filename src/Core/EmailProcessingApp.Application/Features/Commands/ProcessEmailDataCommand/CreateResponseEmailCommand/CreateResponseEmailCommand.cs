using MediatR;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.CreateResponseEmailCommandNamespace
{
    public class CreateResponseEmailCommand : IRequest<CreateResponseEmailCommandResponse>
    {
        public string Email { get; }
        public List<string> Attributes { get; }

        public CreateResponseEmailCommand(string email, List<string> attributes)
        {
            Email = email;
            Attributes = attributes;
        }
    }
}
