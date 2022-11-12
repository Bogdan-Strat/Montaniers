using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Implementation.Account.Models;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.DataAccess;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Validations
{
    public class EditUserModelValidation : AbstractValidator<EditUserModel>
    {
        public UnitOfWork UnitOfWork { get; set; }
        public CurrentUserDto CurrentUser { get; set; }
        public EditUserModelValidation(UnitOfWork unitOfWork, CurrentUserDto currentUserDto)
        {
            UnitOfWork = unitOfWork;
            CurrentUser = currentUserDto;

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Mandatory field!")
                .MustAsync((model, email, _) => EmailNotAlreadyExist(email, model.UserId)).WithMessage("Email address is already taken")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.RoleId)
                .Must(id => Enum.IsDefined((RoleTypes)id)).WithMessage("Invalid")
                .NotEmpty().WithMessage("Mandatory field!");
            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Mandatory field!")
                .MustAsync((model, username, _) => UsernameNotAlreadyExist(username, model.UserId)).WithMessage("Username is already taken");
        }

        public async Task<bool> EmailNotAlreadyExist(string email, Guid userId)
        {
            var oldEmail = await UnitOfWork.Users
                .Get()
                .Where(u => u.UserId == userId)
                .Select(u => u.Email)
                .SingleOrDefaultAsync();

            if (oldEmail == null)
            {
                throw new NotFoundErrorException();
            }

            var res = await UnitOfWork.Users
                .Get()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (res == null)
            {
                return true;
            }
            else
            {
                if(res.Email == oldEmail)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> UsernameNotAlreadyExist(string username, Guid userId)
        {
            var oldUsername = await UnitOfWork.Users
                .Get()
                .Where(u => u.UserId == userId)
                .Select(u => u.Username)
                .SingleOrDefaultAsync();

            if (oldUsername == null)
            {
                throw new NotFoundErrorException();
            }

            var res = await UnitOfWork.Users
                .Get()
                .SingleOrDefaultAsync(u => u.Username == username);

            if (res == null)
            {
                return true;
            }
            else
            {
                if(res.Username == oldUsername)
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
