﻿using Core.Entities.Concrete;
using Entities.Concrete.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>

    {
        public RentalValidator()
        {

            RuleFor(r => r.Id)
                .NotEmpty()
                .WithMessage("Please specify a car name");


            RuleFor(r => r.CarId)
                .NotEmpty();


        }

    }
}
