using Tandem.Responses;
using FluentValidation;

namespace Tests.Validators
{
    public class GetUserResponseValidator : AbstractValidator<GetUserResponse>
    {
        public GetUserResponseValidator()
        {
            RuleFor(user => user.EmailAddress).NotNull().MinimumLength(3);
            RuleFor(user => user.PhoneNumber).NotNull().MinimumLength(7);
            RuleFor(user => user.Name).NotNull().MinimumLength(3);
            RuleFor(user => user.UserId).NotNull().MinimumLength(7);
        }
    }
}
