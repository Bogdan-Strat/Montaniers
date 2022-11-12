using FluentValidation;
using Montaniarzii.BusinessLogic.Implementation.Warnings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Validation
{
    public class CreateWarningModelValidation : AbstractValidator<CreateWarningModel>
    {
        public CreateWarningModelValidation()
        {
            RuleFor(r => r.WarningMessage)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.EndDate)
                .NotEmpty().WithMessage("Mandatory field")
                .Must(IsInFuture).WithMessage("End date must be in future");
        }

        private bool IsInFuture(DateTime endDate)
        {
            return endDate > DateTime.Now;
        }
    }
}
