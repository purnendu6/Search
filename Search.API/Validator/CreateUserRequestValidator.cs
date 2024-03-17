using FluentValidation;
using Search.Application.Commands;

namespace Search.API.Validator
{
    /// <summary>
    /// Validate CreateUserCommandRequest
    /// </summary>
    public class CreateUserRequestValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("UserName is mandatory.")
                .NotEmpty().WithMessage("UserName should not be empty.")
                .MinimumLength(5).WithMessage("Password should be atleast 6 characters.");
            RuleFor(x => x.Password).NotNull().WithMessage("UserName is required.")
                    .NotEmpty().WithMessage("mandatory should not be empty.")
                    .MinimumLength(6).WithMessage("Password should be atleast 5 characters.");
        }
    }
}
