using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Implementation.Trips.Models;
using Montaniarzii.DataAccess;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Validation
{
    public class CreateTripListOfAttractionsValidator : AbstractValidator<TripXAttractionModel>
    {
        public UnitOfWork UnitOfWork { get; set; }
        public CreateTripListOfAttractionsValidator(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            RuleFor(r => r.Duration)
                .Must(NotEmptyDuration).WithMessage("Mandatory field or positive number!");
            RuleFor(r => r.Duration)
                .Must(IsInteger).When(model => NotEmptyDuration(model.Duration)).WithMessage("Duration must be a positive integer");
            RuleFor(r => r.Duration)
                .InclusiveBetween(1, 23).When(model => NotEmptyDuration(model.Duration)).WithMessage("Duration must be between 1 and 23");
            RuleFor(r => r.MarkingId)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.MarkingId)
                .Must(id => Enum.IsDefined((MarkingTypes)id)).When(model => NotEmptyMarking(model.MarkingId)).WithMessage("Invalid");
            RuleFor(r => r.AttractionId)
                .NotEmpty().WithMessage("Mandatory field!");
            
            RuleFor(r => r.AttractionId)
                .MustAsync((model, attractionId, _) => IsAttractionValid(attractionId))
                .When(model => NotEmptyAttraction(model.AttractionId))
                .WithMessage("Attraction is not in our database. Try to suggest it.");
        }

        private bool IsInteger(decimal duration)
        {
            return duration == (int)duration;
        }

        private bool NotEmptyAttraction(Guid attractionId)
        {
            return attractionId != Guid.Empty;
        }

        private async Task<bool> IsAttractionValid(Guid attractionId)
        {
            var attraction = await UnitOfWork.Attractions
                .Get()
                .SingleOrDefaultAsync(a => a.AttractionId == attractionId && a.IsDeleted == false);

            return attraction != null;
        }

        private bool EmptyAttraction(Guid attractionId)
        {
            return attractionId == Guid.Empty;
        }

        private bool NotEmptyMarking(byte markingId)
        {
            return markingId > 0;
        }

        private bool NotEmptyDuration(decimal duration)
        {
            return duration > 0 ;
        }
    }
}
