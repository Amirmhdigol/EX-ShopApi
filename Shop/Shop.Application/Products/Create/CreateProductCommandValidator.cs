﻿using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("title"));
            
            RuleFor(t => t.Slug)
                .NotEmpty().WithMessage(ValidationMessages.required("Slug"));
          
            RuleFor(t => t.Description)
                .NotEmpty().WithMessage(ValidationMessages.required("Descriptin"));
            
            RuleFor(a => a.ImageFile)
                .JustImageFile();
        }
    }
}
