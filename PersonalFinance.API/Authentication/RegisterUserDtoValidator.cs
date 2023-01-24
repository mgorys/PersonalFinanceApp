using FluentValidation;
using PersonalFinance.Models.Dtos;
using PersonalFinance.Persistence;

namespace PersonalFinance.API.Authentication
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterUserDtoValidator(PFDbContext dbContext)
        {
            RuleFor(x => x.Name)
                 .Custom((value, context) =>
                 {
                     var nameInUse = dbContext.Users.Any(u => u.Name == value);
                     if (nameInUse)
                     {
                         context.AddFailure("Name", "That name is in use");
                     }
                 });
            RuleFor(x => x.Password)
                .MinimumLength(5);
            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is in use");
                    }
                });
        }
    }
}
