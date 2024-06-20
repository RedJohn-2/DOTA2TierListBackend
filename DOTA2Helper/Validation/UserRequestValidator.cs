﻿using DOTA2TierList.API.Contracts;
using DOTA2TierList.API.Contracts.UserContracts;
using DOTA2TierList.Logic.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Application.Validation
{
    public class UserRequestValidator : AbstractValidator<IUserRequest>
    {
        public UserRequestValidator() 
        {
            RuleFor(x => x).SetInheritanceValidator(v =>
            {
                v.Add(new CreateUserRequestValidator());
            });

        }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty().Length(2, 15);
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
            RuleFor(request => request.Password).NotEmpty().MinimumLength(8).Equal(request => request.ConfirmPassword);
        }
    }
}
