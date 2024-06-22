using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.Application.Contracts;
using DOTA2TierList.Logic.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.API.Validation
{
    public class UserRequestValidator : AbstractValidator<IUserRequest>
    {
        public UserRequestValidator() 
        {
            RuleFor(x => x).SetInheritanceValidator(v =>
            {
                v.Add(new RegisterUserRequestValidator());
                v.Add(new LoginUserRequestValidator());
            });
        }
    }

    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty().Length(2, 15);
            RuleFor(request => request.Email).NotEmpty().EmailAddress().WithMessage("Email address incorrect!!!");
            RuleFor(request => request.Password).NotEmpty().MinimumLength(8).Equal(request => request.ConfirmPassword);
        }
    }

    public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
            RuleFor(request => request.Password).NotEmpty().MinimumLength(8);
        }
    }
}
