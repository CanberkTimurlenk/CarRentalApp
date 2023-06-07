using Entities.Concrete.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>

    {
        public CarValidator()
        {
            
            
            RuleFor(c => c.CarName)
                .NotEmpty()
                .WithMessage("Please specify a car name");



            RuleFor(c => c.DailyPrice)
                .GreaterThan(0)
                .WithMessage("Daily price must be greater than 0");


        }

    }
}
