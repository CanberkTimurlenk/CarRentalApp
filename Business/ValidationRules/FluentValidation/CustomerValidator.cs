using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>

    {
        public CustomerValidator()
        {

            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Please specify a customer id");


            RuleFor(c => c.CompanyName)
                .MinimumLength(2)
                .When(c => c.CompanyName is not null);  //  The Customer might not have a CompanyName



        }

    }
}
