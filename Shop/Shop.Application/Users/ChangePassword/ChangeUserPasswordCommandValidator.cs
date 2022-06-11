using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Users.ChangePassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور فعلی"))
            .MinimumLength(5).WithMessage(ValidationMessages.required("کلمه عبور فعلی"));
    }
}