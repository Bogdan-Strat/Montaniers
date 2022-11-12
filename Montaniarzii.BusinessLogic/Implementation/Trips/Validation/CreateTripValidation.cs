using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Implementation.Trips.Models;
using Montaniarzii.Common.DTOs;
using Montaniarzii.DataAccess;
using Montaniarzii.Entities.Entities;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Validation
{
    public class CreateTripValidation : AbstractValidator<CreateTripModel>
    {
        public UnitOfWork UnitOfWork { get; set; }
        private readonly string AvatarFilePath = @"C:\Users\bogdan.strat\PozeProiect";
        public CreateTripValidation(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            //RuleFor(r => r.Description)
            //    .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.Equipment)
                .NotEmpty().WithMessage("Mandatory field!")
                .MaximumLength(500).WithMessage("Equipment Message should be shorter");
            RuleFor(r => r.TripDate)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.DifficultyId)
                .LessThanOrEqualTo(r => 10).WithMessage("Invalid")
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.RatingId)
                .LessThanOrEqualTo(r => 5).WithMessage("Invalid")
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.PrivacyId)
                .Must(id => Enum.IsDefined((PublicityTypes)id)).WithMessage("Invalid")
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.TypePostId)
                .Must(id => Enum.IsDefined((PostTypes)id)).WithMessage("Invalid")
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.Attractions)
                .NotEmpty().WithMessage("Invalid")
                .Must(IsValidAttractions).WithMessage("You can have between 2 and 9 attractions");
            RuleForEach(a => a.Attractions).SetValidator(new CreateTripListOfAttractionsValidator(UnitOfWork));
            RuleForEach(u => u.UsersId)
                .MustAsync((model, userId, _) => IsUserValid(userId)).When(model => IsInvitationsNotEmpty(model.UsersId)).WithMessage("User is not valid");
            RuleForEach(p => p.Photos)
                .Must(IsPhotoOnDisk).When(model => NotEmptyPhoto(model.Photos)).WithMessage("Photos must be ulpoaded by button");

        }

        private bool IsPhotoOnDisk(GuidSelectListItemModel<Photo> photo)
        {
            var dir = Path.Combine(AvatarFilePath, photo.Id.ToString());

            if (!Directory.Exists(dir))
            {
                return false;
            }

            var file = Path.Combine(dir, photo.Name);

            if (!File.Exists(file))
            {
                return false;
            }

            return true;
        }

        private bool NotEmptyPhoto(List<GuidSelectListItemModel<Photo>> photos)
        {
            return photos.Count > 0;
        }

        private bool IsInvitationsNotEmpty(List<Guid> usersId)
        {
            return usersId.Count > 0;
        }

        private async Task<bool> IsUserValid(Guid userId)
        {
            var user = await UnitOfWork.Users
                .Get()
                .SingleOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        private bool IsMarkingNotEmpty(byte markingId)
        {
            return markingId > 0;
        }

        private bool IsDurationNotEmpty(int duration)
        {
            return duration > 0;
        }

        private bool NotEmptyAttraction(Guid attractionId)
        {
            return attractionId != Guid.Empty;
        }

        private bool IsDurationValid(int duration)
        {
            return duration > 0 && duration < 24;
        }

        private bool IsValidAttractions(List<TripXAttractionModel> listOfAttraction)
        {
            return listOfAttraction.Count >= 2 && listOfAttraction.Count <=9;
        }

        private async Task<bool> IsAttractionIdValid(Guid attractionId)
        {
            var attraction =  await UnitOfWork.Attractions
                .Get()
                .SingleOrDefaultAsync(a => a.AttractionId == attractionId && a.IsDeleted == false);

            return attraction != null;
        }

    }
}
