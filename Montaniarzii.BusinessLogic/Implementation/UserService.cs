using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Account.Models;
using Montaniarzii.BusinessLogic.Implementation.Account.Validations;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Common.Extensions;
using Montaniarzii.DataAccess;
using Montaniarzii.Entities.Entities;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account
{
    public class UserService : BaseService
    {
        private readonly RegisterUserValidation RegisterUserValidator;
        private readonly EditUserAsUserValidator EditUserAsUserValidator;
        private readonly EditUserModelValidation EditUserModelValidation;
        public UserService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            RegisterUserValidator = new RegisterUserValidation(UnitOfWork);
            EditUserAsUserValidator = new EditUserAsUserValidator(UnitOfWork, CurrentUser);
            EditUserModelValidation = new EditUserModelValidation(UnitOfWork, CurrentUser);
        }

        public async Task RegisterNewUser(RegisterModel model)
        {
            await ExecuteInTransactionAsync(async unitOfWork =>
            {
                RegisterUserValidator.Validate(model).ThenThrow(model);
                Photo? avatarPhoto;
                if (model.ProfilePhoto == null)
                {
                    avatarPhoto = null;
                }
                else
                {
                    avatarPhoto = await ProcessPhotoProfile(model.ProfilePhoto);
                }

                
                var user = Mapper.Map<RegisterModel, User>(model);

                user.RoleId = (int)RoleTypes.User;
                user.IsDeleted = false;
                user.RegisteredDate = DateTime.Now;
                user.HashedPassword = HashedPassword(model.Password);
                user.AvatarPhotos.Add(new AvatarPhoto()
                {
                    UserId = user.UserId,
                    PhotoId = avatarPhoto.PhotoId
                });

                unitOfWork.Users.Insert(user);
                await unitOfWork.SaveChangesAsync();
            });

        }

        public string HashedPassword(string password)
        {
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public CurrentUserDto Login(string email, string password)
        {
            var hashedPassword = HashedPassword(password);

            var user = UnitOfWork.Users
                .Get()
                .Include(u => u.AvatarPhotos)
                    .ThenInclude(ap => ap.Photo)
                .Where(u => u.Email == email && u.HashedPassword == hashedPassword && u.IsDeleted == false)
                .SingleOrDefault();

            if (user == null)
            {
                return new CurrentUserDto { IsAuthenticated = false };
            }

            return new CurrentUserDto
            {
                Id = user.UserId,
                Email = user.Email,
                Username = user.Username,
                IsAuthenticated = true,
                Role = Enum.GetName(typeof(RoleTypes), user.RoleId),
                PhotoId = user.AvatarPhotos.ToList()[0].PhotoId.ToString(),
                PhotoPath = user.AvatarPhotos.ToList()[0].Photo.Path,
            };
        }

        public async Task<ListOfUsersModel> GetAllUsers(int pageNumber = 0)
        {
            if (pageNumber > 0)
            {
                pageNumber--;
            }

            var model = new ListOfUsersModel()
            {
                NumberOfUsers = await UnitOfWork.Users
                    .Get()
                    .CountAsync(),
                ActualPageNumber = pageNumber + 1
            };

            model.Users = await UnitOfWork.Users
                .Get()
                .Include(u => u.Role)
                .OrderBy(u => u.Username)
                .Skip(pageNumber * 10)
                .Take(10)
                .ToListAsync();

            return model;
        }

        public async Task UpdateUserProfile(EditUserModel model)
        {
            (await EditUserModelValidation.ValidateAsync(model)).ThenThrow(model);

            var oldUser = GetUserByIdForUser(model.UserId);

            if (oldUser == null)
            {
                throw new NotFoundErrorException();
            }

            Mapper.Map(model, oldUser);
            oldUser.RoleId = model.RoleId;

            UnitOfWork.Users.Update(oldUser);
            await UnitOfWork.SaveChangesAsync();
        }


        public EditUserModel? GetUserByIdForEditUserModel(Guid id)
        {
            var user = UnitOfWork.Users
                .Get()
                .SingleOrDefault(u => u.UserId == id);

            if (user == null)
            {
                throw new NotFoundErrorException();
            }

            return new EditUserModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async  Task<EditUserProfileAsUserModel?> GetUserByIdForEditUserModelAsUser()
        {
            var user = await UnitOfWork.Users
                .Get()
                .SingleOrDefaultAsync(u => u.UserId == CurrentUser.Id);

            if (user == null)
            {
                throw new NotFoundErrorException();
            }

            return new EditUserProfileAsUserModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public User GetUserByIdForUser(Guid id)
        {
            var user = UnitOfWork.Users
                .Get()
                .SingleOrDefault(u => u.UserId == id);

            if(user == null)
            {
                throw new NotFoundErrorException();
            }

            return user;
        }

        public async Task SoftDeleteUser(Guid userId)
        {
            var user = UnitOfWork.Users
                .Get()
                .SingleOrDefault(u => u.UserId == userId);

            if(user == null)
            {
                throw new NotFoundErrorException();
            }

            user.IsDeleted = true;

            UnitOfWork.Users.Update(user);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UndoSoftDeleteUser(Guid userId)
        {
            var user = UnitOfWork.Users
                .Get()
                .SingleOrDefault(u => u.UserId == userId);

            if(user == null)
            {
                throw new NotFoundErrorException();
            }

            user.IsDeleted = false;

            UnitOfWork.Users.Update(user);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<GuidSelectListItemModel<User>>> GetAllUsersByUsername(string partOfName)
        {
            var users = await UnitOfWork.Users
                .Get()
                .Where(a => a.IsDeleted == false && a.Username != CurrentUser.Username && a.RoleId != 1)
                
                .ToListAsync();

            return users
                .Where(u => u.Username.ToLower().RemoveDiacritics().ManualContains(partOfName.ToLower().RemoveDiacritics()))
                .Select(a => new GuidSelectListItemModel<User>()
                {
                    Id = a.UserId,
                    Name = a.Username
                })
                .ToList();
        }

        public async Task<string> GetUsernameById(Guid userId)
        {
            var username = await UnitOfWork.Users
                .Get()
                .Where(u => u.UserId == userId)
                .Select(u => u.Username)
                .SingleOrDefaultAsync();

            if (username == null)
            {
                throw new NotFoundErrorException();
            }

            return username;
        }

        private async Task<User> GetUserById(Guid userId)
        {
            var query = UnitOfWork.Users
                .Get()
                .Include(u => u.AvatarPhotos)
                    .ThenInclude(ap => ap.Photo);

            var user = await query
                .FirstOrDefaultAsync(u => u.UserId == userId && u.IsDeleted == false);

            if(user == null)
            {
                throw new NotFoundErrorException();
            }

            return user;
        }

        public async Task<UserProfileModel> GetProfilePage(Guid userId)
        {
            var user = await GetUserById(userId);
            var avatarPhotoPath = user.AvatarPhotos.First().Photo.Path;
                    //.Where(ap => ap.Photo.IsDeleted == false)

            if(avatarPhotoPath == null)
            {
                throw new NotFoundErrorException();
            }

            var model = new UserProfileModel()
            {
                UserId = userId,
                Username = user.Username,
                AvatarPhotoPath = avatarPhotoPath,
                AvatarPhotoId = user.AvatarPhotos
                    .Select(ap => ap.PhotoId)
                    .SingleOrDefault()
            };

            return model;
        }

        public async Task<Photo> ProcessPhotoProfile(IFormFile photo)
        {
            var photoId = Guid.NewGuid();
            var dir = Path.Combine(AvatarFilePath, photoId.ToString());

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (photo.Length > 0)
            {
                string filePath = Path.Combine(dir, photo.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                var avatarPhoto = new Photo()
                {
                    PhotoId = photoId,
                    Path = photo.FileName,
                    IsDeleted = false
                };
                UnitOfWork.Photos.Insert(avatarPhoto);
                await UnitOfWork.SaveChangesAsync();

                return avatarPhoto;
            }

            return null;
        }

        public async Task EditUserProfileAsUser(EditUserProfileAsUserModel model)
        {
            //try
            //{
                await ExecuteInTransactionAsync(async unitOfWork =>
                {
                    (await EditUserAsUserValidator.ValidateAsync(model)).ThenThrow(model);

                    var user = await GetUserById(model.UserId);

                    if(user == null)
                    {
                        throw new NotFoundErrorException();
                    }

                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    if (model.NewPassword != null)
                        user.HashedPassword = HashedPassword(model.NewPassword);

                    if(model.NewProfilePicture != null)
                    {
                        var avatarPhoto = await ChangeUserPhoto(user, model.NewProfilePicture);

                        if (avatarPhoto != null)
                        {
                            user.AvatarPhotos.Add(new AvatarPhoto()
                            {
                                UserId = user.UserId,
                                PhotoId = avatarPhoto.PhotoId
                            });

                        }

                    }
                    unitOfWork.Users.Update(user);
                    await unitOfWork.SaveChangesAsync();

                });
            //}
            //catch(Exception e)
            //{
            //    // sterg directorul
            //    throw e;
            //}
            
            


        }

        private async Task<Photo?> ChangeUserPhoto(User user, IFormFile profilePicture)
        {
            var photoId = user.AvatarPhotos
                .Select(ap => ap.PhotoId)
                .FirstOrDefault();

            if(photoId == Guid.Empty)
            {
                throw new NotFoundErrorException();
            }

            if (photoId != Guid.Empty)
            {
                var photo = await UnitOfWork.Photos
                    .Get()
                    .Include(p => p.AvatarPhotos)
                    .Where(p => p.PhotoId == photoId)
                    .SingleOrDefaultAsync();

                photo.AvatarPhotos.Clear();
                UnitOfWork.Photos.HardDelete(photo);
                await UnitOfWork.SaveChangesAsync();
            }


            return await ProcessPhotoProfile(profilePicture);

        }

        public async Task<bool> IsUsernameValid(string username)
        {
            var users = await UnitOfWork.Users
                .Get()
                .ToListAsync();

            var userWithoutDiacritics = users
                .Where(u => u.Username.ToLower().RemoveDiacritics() == username.ToLower().RemoveDiacritics())
                .SingleOrDefault();

            if(userWithoutDiacritics == null)
            {
                return false;
            }

            return true;
        }



    }
}
