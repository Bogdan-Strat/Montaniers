using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    public class TrainStationController : BaseController
    {
        private readonly TrainStationService Service;
        public TrainStationController(ControllerDependencies dependencies, TrainStationService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetClosestTrainStationsPaged(Guid attractionId, int count = 0)
        {
            var trainStations = await Service.GetClosestTrainsStationsPaged(attractionId, count);

            return Ok(trainStations);
        }

        [HttpGet]
        public IActionResult GetViewForSearchingClosestsTrainStations()
        {
            return View();
        }




    }
}
