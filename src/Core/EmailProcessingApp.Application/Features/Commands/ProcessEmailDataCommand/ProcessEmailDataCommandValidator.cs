using EmailProcessingApp.Application.Contract.Persistence;
using EmailValidation;
using FluentValidation;

namespace EmailProcessingApp.Application.Features.Commands.ProcessEmailDataCommand
{
    public class ProcessEmailDataCommandValidator : AbstractValidator<ProcessEmailDataCommand>
    {
        private readonly IEmailDataRepository _repository;

        public ProcessEmailDataCommandValidator(IEmailDataRepository repository)
        {
            _repository = repository;

            RuleFor(e => e).Must((e) => e.EmailDataDto.Attributes.Distinct().Count() == e.EmailDataDto.Attributes.Count).WithMessage("Duplicate attribute values are not allowed.");
            RuleFor(e => e).Must((e) => EmailValidator.Validate(e.EmailDataDto.Email)).WithMessage("Provided email address is not in the valid format.");
            RuleFor(e => e).MustAsync(IsUniqueAsync).WithMessage("The unique key provided is already present in the database!");
            RuleFor(e => e).MustAsync(IsAttributeListUnique).WithMessage($"One or more of the provided attributes have been already saved for {DateTime.UtcNow.ToShortDateString()}.");
            
            // ... define additional client request constraints if required ...
        }

        private async Task<bool> IsUniqueAsync(ProcessEmailDataCommand command, CancellationToken token)
        {
            return !await _repository.DoesExistAsync(command.EmailDataDto.Key);
        }

        private async Task<bool> IsAttributeListUnique(ProcessEmailDataCommand command, CancellationToken token)
        {
            return await _repository.ValidateAttributeListAsync(command.EmailDataDto.Email, command.EmailDataDto.Attributes);
        }
    }
}
