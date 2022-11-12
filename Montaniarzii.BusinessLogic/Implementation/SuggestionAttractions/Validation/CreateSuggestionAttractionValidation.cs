using FluentValidation;
using Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Validation
{
    public class CreateSuggestionAttractionValidation : AbstractValidator<CreateSuggestionAttractionModel>
    {
        public CreateSuggestionAttractionValidation()
        {
            RuleFor(r => r.AttractionName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.TypeAttractionId)
                .NotEmpty().WithMessage("Mandatory field!")
                .Must(id => Enum.IsDefined((AttractionTypes)id))
                .WithMessage("Invalid!");
            RuleFor(r => r.Location)
                .MaximumLength(500).WithMessage("Maximum length is 500 characters");
            RuleFor(r => r.Height)
                .Must(IsValid).WithMessage("Height must be an integer or Height is not between 0 and 8849");
            RuleFor(r => r.Mountains)
                .MaximumLength(100).WithMessage("Invalid!");
            RuleFor(r => r.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");
            RuleFor(r => r.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180!"); 

        }

        private bool IsValid(string height)
        {
            if (height == "")
                return true;

            try
            {
                var height_num = int.Parse(height);
                return height_num >= 0 && height_num <= 8849;
            }
            catch
            {
                return false;
            }
        }
    }
}
