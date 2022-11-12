using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.Code.Base;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Entities.Entities;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class AttractionController : BaseController
    {
        private readonly AttractionService Service;
        public AttractionController(ControllerDependencies dependencies, AttractionService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetNameOfAttractionsByPartiallyName(string partOfName)
        {

            if (partOfName != null && partOfName.Length >= 3)
            {
                return Ok(await Service.GetAllAttractionsByPartiallyName(partOfName));
            }
            else
            {
                return Ok(new List<GuidSelectListItemModel<Attraction>>());
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractionsWithCoordinatesByPartiallyName(string partOfName)
        {
            if (partOfName != null && partOfName.Length >= 3)
            {
                return Ok(await Service.GetAllAttractionsWithCoordinatesByPartiallyName(partOfName));
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractions(int count = 0)
        {
            var attractions = await Service.GetAllAttractions(count);

            return View(attractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractionsPaged(int count = 1)
        {
            var attractions = await Service.GetAllAttractions(count);

            return Ok(attractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttractionInformation(Guid attractionId)
        {
            var attraction = await Service.GetAttractionInformation(attractionId);

            return View(attraction);
        }

        [HttpGet]
        public async Task<IActionResult> GetNameOfAttractionsPagedByPartiallyName(string partOfName, int count = 0)
        {
            if(partOfName.Length >= 3)
            {
                var attractions = await Service.GetAllAttractionsPagedByPartiallyName(partOfName, count);

                return Ok(attractions);
            }

            else
            {
                return Ok();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GeAllMountains()
        {
            var mountains = await Service.GeAllMountains();

            return Ok(mountains);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttractionsPagedFilteredByMountains(string mountains, int count = 0)
        {
            var attractions = await Service.GetAttractionsPagedFilteredByMountains(mountains, count);

            return Ok(attractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractionsPagedFilteredByPartiallyNameAndMountains(string partOfName, string mountains, int count = 0)
        {
            var attractions = await Service.GetAllAttractionsPagedFilteredByPartiallyNameAndMountains(partOfName, mountains, count);

            return Ok(attractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractionsPagedFilteredByPartiallyNameMountainsAndHeight(string partOfName, string mountains, int height, int count = 0)
        {
            var attractions = await Service.GetAllAttractionsPagedFilteredByPartiallyNameMountainsAndHeight(partOfName, mountains, height, count);

            return Ok(attractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrendingAttractions()
        {
            return Ok(await Service.GetTrendingAttractions());
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxHeightOfAttraction()
        {
            return Ok(await Service.GetMaxHeightOfAttraction());
        }

        [HttpGet]
        public async Task<IActionResult> GetMinHeightOfAttraction()
        {
            return Ok(await Service.GetMinHeightOfAttraction());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation(string partOfName, string mountains, int height, string location, int count = 0)
        {
            var attractions = await Service.GetAllAttractionsPagedFilteredByPartiallyNameMountainsHeightAndLocation(partOfName, mountains, height, location, count);

            return Ok(attractions);
        }
    }
}
