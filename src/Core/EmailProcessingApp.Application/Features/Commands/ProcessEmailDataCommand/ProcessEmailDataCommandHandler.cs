using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Application.Extensions;
using EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.HandleEmailResponseCommand;
using MediatR;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand
{
    /// <summary>
    /// Command handler for process email event sent from controller endpoint.
    /// </summary>
    public class ProcessEmailDataCommandHandler : IRequestHandler<ProcessEmailDataCommand, ProcessEmailDataCommandResponse>
    {
        private readonly IEmailDataRepository _repository;
        private readonly IBlobService _blobService;
        private readonly IMediator _mediator;

        public ProcessEmailDataCommandHandler(IBlobService blobService, IEmailDataRepository repository, IMediator mediator)
        {
            _blobService = blobService;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<ProcessEmailDataCommandResponse> Handle(ProcessEmailDataCommand request, CancellationToken cancellationToken)
        {
            var response = new ProcessEmailDataCommandResponse();

            var validator = new ProcessEmailDataCommandValidator(_repository);
            var validationResult = await validator.ValidateAsync(request);

            // Validating client request and throwing exception if there are validation errors
            validationResult.Resolve(response);

            // Adding email data entry to the table
            await _repository.AddAsync(request.EmailDataDto.ToEmailData());

            // Storing client payload to blob storage
            var blobName = request.EmailDataDto.ToBlobName();
            var content = request.EmailDataDto.ToLogData(response);
            await _blobService.AppendToBlobAsync(blobName, content, BlobContainerType.EmailLogContainer);

            // Handling response email
            await _mediator.Send(new HandleResponseEmailCommand(request.EmailDataDto));

            return response;
        }
    }
}
