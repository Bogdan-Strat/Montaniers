using FluentValidation;
using Microsoft.AspNetCore.Http;
using Montaniarzii.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Validations
{
    public class RegisterUserValidation : AbstractValidator<RegisterModel>
    {
        public UnitOfWork UnitOfWork { get; set; }
        public RegisterUserValidation(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.Email)
                .Must(EmailNotAlreadyExist).WithMessage("Email address is already taken");
            RuleFor(r => r.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).When(model => EmailIsNotEmpty(model.Email)).WithMessage("Email mut be a valid adress");
            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.Username)
                .Must(UsernameNotAlreadyExist).When(model => UsernameIsNotEmpty(model.Username)).WithMessage("Username is already taken");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.Password)
                .Must(PasswordLength).When(model => PasswordIsNotEmpty(model.Password)).WithMessage("Password must have at least 8 characters");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.ProfilePhoto)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.ProfilePhoto)
                .Must(IsPhoto).When(model => PhotoIsNotEmpty(model.ProfilePhoto)).WithMessage("The photo must have the extension.png, .jpg or .jpeg");

        }

        private bool EmailIsNotEmpty(string? email)
        {
            return email != null;
        }

        private bool UsernameIsNotEmpty(string? username)
        {
            return username != null;
        }

        private bool PhotoIsNotEmpty(IFormFile? profilePhoto)
        {
            return profilePhoto != null;
        }

        private bool PasswordIsNotEmpty(string? password)
        {
            return password != null;
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

        public bool EmailNotAlreadyExist(string email)
        {
            var res = UnitOfWork.Users
                .Get()
                .FirstOrDefault(u => u.Email == email);

            if (res == null)
            {
                return true;
            }

            return false;
        }

        public bool UsernameNotAlreadyExist(string username)
        {
            var res = UnitOfWork.Users
                .Get()
                .FirstOrDefault(u => u.Username == username);

            if (res == null)
            {
                return true;
            }

            return false;
        }

        public bool PasswordLength(string password)
        {
            return password.Length >= 8;
        }
    }

    
}
