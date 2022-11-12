using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.BusinessLogic.Implementation.Trips.Models;
using Montaniarzii.Code.Base;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Enums;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class TripController : BaseController
    {
        private readonly TripService TripService;
        private readonly FollowService FollowService; 

        public TripController(ControllerDependencies dependencies, TripService tripService, FollowService followService) : base(dependencies)
        {
            TripService = tripService;
            FollowService = followService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrips(Guid userId)
        {
            var trips = await TripService.GetAllTripsView(userId);

            return View(trips);
        }

        [HttpGet]
        public IActionResult CreateTrip()
        {
            //var enumVal = Enum.GetValues(typeof(PostTypes)).Cast<PostTypes>;

            var model = new CreateTripModel();

            return View("CreateTrip", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip([FromBody]CreateTripModel model)
        {
            if (model == null)
            {
                throw new BadRequestErrorException();
            }

            await TripService.CreateTrip(model);

            return Ok();
        }

        //[HttpGet]
        //public IActionResult EditTrip()
        //{
        //    var model = TripService.EditTrip();

        //    return View(model);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllTripsForAListOfUsers(int count = 0)
        {
            var usersId = await FollowService.GetIdOfPeopleYouFollow();
            var trips = await TripService.GetAllTripsViewForAListOfUsers(usersId, count);

            return View(trips);  
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTripsPagedForAListOfUsers(int count = 0)
        {
            var usersId = await FollowService.GetIdOfPeopleYouFollow();
            var trips = await TripService.GetAllTripsViewForAListOfUsers(usersId, count);

            return Ok(trips);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrip(Guid tripId)
        {
            await TripService.DeleteTrip(tripId);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            var events = await TripService.GetUpcomingEvents();

            return Ok(events); 
        }

        [HttpGet]
        public async Task<IActionResult> GetPastEvents()
        {
            var events = await TripService.GetPastEvents();

            return Ok(events);
        }
    }
}
