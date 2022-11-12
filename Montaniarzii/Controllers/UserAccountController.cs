using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.BusinessLogic.Implementation.Account;
using Montaniarzii.BusinessLogic.Implementation.Account.Models;
using Montaniarzii.Code.Base;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;
using System.Security.Claims;

namespace Montaniarzii.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly UserService UserService;
        private readonly TripService TripService;
        private readonly FollowService FollowService;

        public UserAccountController(ControllerDependencies dependencies, UserService userService, TripService tripService, FollowService followService) : base(dependencies)
        {
            UserService = userService;
            TripService = tripService;
            FollowService = followService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            
            return View("Register", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if(model == null)
            {
                return View("Error");
            }

            await UserService.RegisterNewUser(model);

            return RedirectToAction("Login", "UserAccount");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = UserService.Login(model.Email, model.Password);
           
            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(user);

            return RedirectToAction("GetAllTripsForAListOfUsers", "Trip");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await LogOut();

            return RedirectToAction("Index", "Home");
        }


        private async Task LogIn(CurrentUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", $"{user.Username}"),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("PhotoId", user.PhotoId),
                new Claim("PhotoPath", user.PhotoPath)
            };

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "MontaniarziiCookies",
                    principal: principal);
        }

        [Authorize]
        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "MontaniarziiCookies");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 0)
        {
            var users = await UserService.GetAllUsers(pageNumber);
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserProfile(Guid userId)
        {
            var model = UserService.GetUserByIdForEditUserModel(userId);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUserProfile(EditUserModel model)
        {
            await UserService.UpdateUserProfile(model);

            return RedirectToAction("GetAllUsers", "UserAccount");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDeleteUser(Guid userId)
        {
            await UserService.SoftDeleteUser(userId);

            return RedirectToAction("GetAllUsers", "UserAccount");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UndoSoftDeleteUser(Guid userId)
        {
            await UserService.UndoSoftDeleteUser(userId);

            return RedirectToAction("GetAllUsers", "UserAccount");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserByPartiallyUsername(string partOfName)
        {
           if(partOfName != null && partOfName.Length >= 3)
                return Ok(await UserService.GetAllUsersByUsername(partOfName));
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfilePage(Guid userId)
        {
            if(userId != Guid.Empty)
            {
                var model = await UserService.GetProfilePage(userId);

                model.Trips = await TripService.GetAllTripsView(userId);
                model.statusRequest = await FollowService.GetStatusRequest(userId);

                return View(model);
            }
            else
            {
                throw new NotFoundErrorException();
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUserProfileAsUser()
        {
            var model = await UserService.GetUserByIdForEditUserModelAsUser();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUserProfileAsUser([FromForm] EditUserProfileAsUserModel model)
        {
            await UserService.EditUserProfileAsUser(model);
            var userId = model.UserId;

            return RedirectToAction("GetProfilePage", "UserAccount", new { userId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetManagePage()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IsUsernameValid(string username)
        {
            var result = await UserService.IsUsernameValid(username);

            return Ok(result);
        }

    }
}
