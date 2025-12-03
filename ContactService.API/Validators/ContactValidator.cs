using ContactServices.API.DTOs;
using FluentValidation;

namespace ContactServices.API.Validators
{
    public class ContactValidator: AbstractValidator<ContactRequestDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required!");

            RuleFor(x => x.Mobile)
                .Matches(@"^[0-9]{10}$")
                .WithMessage("Invalid mobile number!");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address!");
        }
    }
}
