using AutoMapper;
using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Application.Extensions;
using MediatR;
using System.Diagnostics;
using System.Net;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand
{
    public class ProcessEmailDataCommandHandler : IRequestHandler<ProcessEmailDataCommand, ProcessEmailDataCommandResponse>
    {
        private readonly IEmailDataRepository _repository;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public ProcessEmailDataCommandHandler(ILogService logService, IMapper mapper, IEmailDataRepository repository)
        {
            _logService = logService;
            _mapper = mapper;
            _repository = repository;
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
                    response.ErrorMessage = ex.Message;
                }
            }

            try
            {
                await _logService.LogEmailDataPayloadAsync(request.EmailDataDto, response);
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"Failed to log incoming request to blob storage:\n{ex.Message}");
            }

            return response;
        }
    }
}
