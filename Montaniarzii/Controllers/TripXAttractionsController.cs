using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class TripXAttractionsController : BaseController
    {
        private readonly TripXAttractionService Service;
        public TripXAttractionsController(ControllerDependencies dependencies, TripXAttractionService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpGet]
        public async  Task<IActionResult> GetTripInformation(Guid tripId)
        {
            var tripInformation = await Service.GetTripInformation(tripId);

            return View(tripInformation);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventInformation(Guid tripId)
        {
            var eventInformation = await Service.GetEventInformation(tripId);

            return View(eventInformation);
        }

        [HttpGet]
        public async Task<IActionResult> GetTripInformationForEdit(Guid tripId)
        {
            var trip = await Service.GetTripInformationForEdit(tripId);

            return View(trip);
        }

    }
}
