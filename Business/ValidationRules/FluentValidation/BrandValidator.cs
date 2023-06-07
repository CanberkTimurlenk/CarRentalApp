using Entities.Concrete.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>

    {
        public BrandValidator()
        {

            RuleFor(b => b.Id)
                .NotEmpty();


            RuleFor(b => b.BrandName)
                .MinimumLength(2);



        }

    }
}
