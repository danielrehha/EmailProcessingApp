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

            // Fetching recorded email data for the current date
            var result = await _repository.GetRangeByEmailAddressAsync(request.EmailDataDto.Email, DateTime.Now, DateTime.Now);

            // Converting attributes for rule assertion
            var attributes = new List<string>();
            var attributeArrays = result
                .Select(e => e.Attributes.Split(","))
                .ToList();
            foreach (var array in attributeArrays)
            {
                attributes.AddRange(array);
            }

            // If there are at least 10 unique attributes we create a response email
            if (attributes.Distinct().Count() >= 10)
            {
                await _mediator.Send(new CreateResponseEmailCommand(request.EmailDataDto.Email, attributes.Take(10).ToList()));
            }

            return response;
        }
    }
}
