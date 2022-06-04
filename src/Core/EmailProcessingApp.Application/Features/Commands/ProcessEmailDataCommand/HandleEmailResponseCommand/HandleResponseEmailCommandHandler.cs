using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.CreateResponseEmailCommandNamespace;
using EmailProcessingApp.Domain.Models;
using MediatR;
using System.Diagnostics;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.HandleEmailResponseCommand
{
    public class HandleResponseEmailCommandHandler : IRequestHandler<HandleResponseEmailCommand, HandleResponseEmailCommandResponse>
    {
        private readonly IEmailDataRepository _repository;
        private readonly IMediator _mediator;

        public HandleResponseEmailCommandHandler(IEmailDataRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<HandleResponseEmailCommandResponse> Handle(HandleResponseEmailCommand request, CancellationToken cancellationToken)
        {
            var response = new HandleResponseEmailCommandResponse();

            var result = await _repository.GetRangeByEmailAddressAsync(request.EmailDataDto.Email, DateTime.Now, DateTime.Now);

            var attributes = new List<string>();

            var attributeArrays = result
                .Select(e => e.Attributes.Split(","))
                .ToList();

            foreach (var array in attributeArrays)
            {
                attributes.AddRange(array);
            }

            if (attributes.Distinct().Count() >= 10)
            {
                await _mediator.Send(new CreateResponseEmailCommand(request.EmailDataDto.Email, attributes.Take(10).ToList()));
            }

            return response;
        }
    }
}
