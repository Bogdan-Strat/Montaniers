using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Implementation.Account.Models;
using Montaniarzii.Common.DTOs;
using Montaniarzii.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Validations
{
    public class EditUserAsUserValidator : AbstractValidator<EditUserProfileAsUserModel>
    {
        public UnitOfWork UnitOfWork { get; set; }
        public CurrentUserDto CurrentUser { get; set; }
        public EditUserAsUserValidator(UnitOfWork unitOfWork, CurrentUserDto currentUserDto)
        {
            UnitOfWork = unitOfWork;
            CurrentUser = currentUserDto;

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Mandatory field!")
                .MustAsync((model, email, _) => EmailNotAlreadyExist(email)).WithMessage("Email address is already taken")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.NewPassword)
                .Must(PasswordLength).When(model => NotEmptyPassword(model.NewPassword)).WithMessage("Password must have at least 8 characters");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.NewProfilePicture)
                .Must(IsPhoto).When(model => NotEmptyPhoto(model.NewProfilePicture)).WithMessage("The photo must have extension .jpg, .jpeg, or .png");

        }

        private bool NotEmptyPassword(string newPassword)
        {
            return newPassword != null;
        }

        private bool NotEmptyPhoto(IFormFile newProfilePicture)
        {
            return newProfilePicture != null;
        }

        private bool IsPhoto(IFormFile photo)
        {
            var photoExtension = photo.FileName.Split('.').LastOrDefault();
            var validExtensions = new List<string>() { "jpg", "png", "jpeg" };

            if (photoExtension == null)
            {
                return false;
            }

            return validExtensions.Contains(photoExtension);


        }
        public async Task<bool> EmailNotAlreadyExist(string email)
        {
            var res = await UnitOfWork.Users
                .Get()
                .SingleOrDefaultAsync(u => u.Email == email);

            if (res == null)
            {
                return true;
            }
            else
            {
                if(res.Email == CurrentUser.Email)
                {
                    return true;
                }
            }

            return false;
        }

        public bool PasswordLength(string password)
        {
            if (password != null)
                return password.Length >= 8;

            return true;
        }
    }
}
