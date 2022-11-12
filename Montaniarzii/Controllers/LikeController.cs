using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class LikeController : BaseController
    {
        private readonly LikeService Service;

        public LikeController(ControllerDependencies dependencies, LikeService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpPost]
        public async Task<IActionResult> LikeTrip(Guid tripId)
        {
            await Service.LikeTrip(tripId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> IsTripLikedByCurrentUser(Guid tripId)
        {
            var isLiked = await Service.IsTripLikedByCurrentUser(tripId);

            return Ok(isLiked);
        }

        [HttpGet]
        public async Task<IActionResult> GetNumberOfLikesForATrip(Guid tripId)
        {
            var numberOfLikes = await Service.GetNumberOfLikesForATrip(tripId);

            return Ok(numberOfLikes);
        }

        [HttpPost]
        public async Task<IActionResult> DislikeTrip(Guid tripId)
        {
            await Service.DislikeTrip(tripId);

            return Ok();
        }
    }
}
