using AuthServer.Core.Dto;
using FluentValidation;

namespace AuthServer.API.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(i => i.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(i => i.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(i => i.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Email is wrong");
        }
    }
}
