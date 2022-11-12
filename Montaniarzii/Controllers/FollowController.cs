using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.BusinessLogic.Implementation.Account;
using Montaniarzii.BusinessLogic.Implementation.Follows.Models;
using Montaniarzii.Code.Base;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class FollowController : BaseController
    {
        private readonly FollowService FollowService;
        private readonly UserService UserService;

        public FollowController(ControllerDependencies dependencies, FollowService followService, UserService userService) : base(dependencies)
        {
            FollowService = followService;
            UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestFollow([FromBody] RequestFollowModel model)
        {
            await FollowService.RequestFollow(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusRequest(Guid followedUserId)
        {
            var statusRequest = await FollowService.GetStatusRequest(followedUserId);

            return Ok(statusRequest);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingFollowRequests()
        {
            var followRequests = await FollowService.GetAllPendingFollowsRequests();

            followRequests.ForEach(async fr => fr.FollowingUsername = await UserService.GetUsernameById(fr.FollowingUserId));

            return View(followRequests);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingFollowRequestsForJS()
        {
            var followRequests = await FollowService.GetAllPendingFollowsRequests();

            if (followRequests == null)
            {
                throw new NotFoundErrorException();
            }
            // followRequests.ForEach(async fr => fr.FollowingUsername = await UserService.GetUsernameById(fr.FollowingUserId));

            return Ok(followRequests);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveFollowRequest(Guid followingUserId)
        {
            await FollowService.ApproveFollowRequest(followingUserId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DenyFollowRequest(Guid followingUserId)
        {
            await FollowService.DenyFollowRequest(followingUserId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleThatYouFollow()
        {
            var peopleThatYouFollow = await FollowService.GetPeopleThatYouFollow();

            return View(peopleThatYouFollow);
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleThatYouFollowForJs()
        {
            var peopleThatYouFollow = await FollowService.GetPeopleThatYouFollow();

            return Ok(peopleThatYouFollow);
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleThatFollowYou()
        {
            var peopleThatFollowYou = (await FollowService.GetPeopleThatFollowYou());

            return View(peopleThatFollowYou);
        }

        [HttpGet]
        public async Task<IActionResult> GetPeopleThatFollowYouForJS()
        {
            var peopleThatFollowYou = (await FollowService.GetPeopleThatFollowYou());

            return Ok(peopleThatFollowYou);
        }

        [HttpPost]
        public async Task<IActionResult> Unfollow(Guid followedUserId)
        {
            await FollowService.Unfollow(followedUserId);

            return Ok();
        }


    }
}
