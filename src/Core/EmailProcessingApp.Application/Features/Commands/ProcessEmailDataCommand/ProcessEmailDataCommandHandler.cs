using AutoMapper;
using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Application.Extensions;
using EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.HandleEmailResponseCommand;
using MediatR;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand
{
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

            validationResult.Resolve(response);

            if (response.Success)
            {
                try
                {
                    await _repository.AddAsync(request.EmailDataDto.ToEmailData());
                }
                catch (Exception ex)
                {
                    response.HttpStatusCode = HttpStatusCode.InternalServerError;
                    response.ErrorMessage = "Unexpected error.";

                    Trace.TraceError($"Failed to save email data to database:\n{ex.Message}");
                }
            }

            try
            {
                var blobName = request.EmailDataDto.ToBlobName();
                var content = request.EmailDataDto.ToLogData(response);
                await _blobService.AppendToBlobAsync(blobName, content, BlobContainerType.EmailLogContainer);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Failed to log incoming request to blob storage:\n{ex.Message}");
            }

            if(response.Success)
            {
                await _mediator.Send(new HandleResponseEmailCommand(request.EmailDataDto));
            }

            return response;
        }
    }
}
