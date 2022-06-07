using EmailProcessingApp.Application.Contract.Logging;
using EmailProcessingApp.Application.Contract.Persistence;
using EmailProcessingApp.Application.Contract.Static;
using EmailProcessingApp.Application.Enums;
using EmailProcessingApp.Domain.Models;
using MediatR;
using System.Text;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand.CreateResponseEmailCommandNamespace
{
    public class CreateResponseEmailCommandHandler : IRequestHandler<CreateResponseEmailCommand, CreateResponseEmailCommandResponse>
    {
        private readonly IBlobService _blobService;
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IResponseEmailRepository _repository;

        public CreateResponseEmailCommandHandler(IBlobService blobService, IMessageTemplateService messageTemplateService, IResponseEmailRepository repository)
        {
            _blobService = blobService;
            _messageTemplateService = messageTemplateService;
            _repository = repository;
        }

        public async Task<CreateResponseEmailCommandResponse> Handle(CreateResponseEmailCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateResponseEmailCommandResponse();

            // Fetching response email for the current date
            var email = await _repository.FindByEmailAndDateAsync(request.Email, DateTime.Now);

            // If we have already created one previously we close the event without action
            if (email != null)
            {
                return response;
            }

            // We fetch the custom message template from the appropriate service and then format it correctly
            var messageTemplate = await _messageTemplateService.GetMessageTemplateAsync(MessageTemplateType.ResponseEmailBody);
            var messageBody = messageTemplate.Replace("{}", string.Join(",", request.Attributes));

            // Inserting the response email in the sendemails table
            await _repository.AddAsync(new ResponseEmail() { Email = request.Email, EmailBody = Encoding.UTF8.GetBytes(messageBody), IsSent = false });

            // Storing the response email to blob storage in the appropriate container
            var blobName = $"{DateTime.Now.ToShortDateString().Replace("/", "-")}_{request.Email}.txt";
            await _blobService.AppendToBlobAsync(blobName, messageBody, BlobContainerType.EmailBodyContainer);

            return response;
        }
    }
}
