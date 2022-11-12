using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class SuggestionAttractionController : BaseController
    {
        private readonly SuggestionAttractionService Service;
        public SuggestionAttractionController(ControllerDependencies dependencies, SuggestionAttractionService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuggestionAttraction([FromBody]CreateSuggestionAttractionModel model)
        {
            await Service.CreateSuggestionAttraction(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYourSuggestionAttraction()
        {
            var suggestionAttractions = await Service.GetAllYourSuggestionAttraction();

            return View(suggestionAttractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllYourSuggestionAttractionForJS()
        {
            var suggestionAttractions = await Service.GetAllYourSuggestionAttraction();

            return Ok(suggestionAttractions);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSuggestionAttractions(int pageNumber = 0)
        {
            var suggestionAttractions = await Service.GetAllSuggestionAttractions(pageNumber);

            return View(suggestionAttractions);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuggestionAttractionById(Guid suggestionAttractionId)
        {
            var suggestionAttraction = await Service.GetSuggestionAttractionById(suggestionAttractionId);

            return View(suggestionAttraction);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSuggestionAttractionByIdAsAdmin(Guid suggestionAttractionId)
        {
            var suggestionAttraction = await Service.GetSuggestionAttractionByIdAsAdmin(suggestionAttractionId);

            return Ok(suggestionAttraction);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveSuggestion([FromBody] ApproveSuggestionModel model)
        {
            await Service.ApproveSuggestion(model);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectSuggestion(Guid suggestionAttractionId)
        {
            await Service.RejectSuggestionAttraction(suggestionAttractionId);

            return RedirectToAction("GetAllSuggestionAttractions", "SuggestionAttraction");
        }


    }
}
