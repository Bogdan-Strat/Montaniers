using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.BusinessLogic.Implementation.Warnings.Models;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class WarningController : BaseController
    {
        private readonly WarningService Service;
        public WarningController(ControllerDependencies dependencies, WarningService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateWarning()
        {
            var model = new CreateWarningModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateWarning(CreateWarningModel model)
        {
            await Service.CreateWarning(model);

            return RedirectToAction("GetWarningsAsAdmin");
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveWarningsAsUser()
        {
            return Ok(await Service.GetActiveWarningsAsUser());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetWarningsAsAdmin(int count = 0)
        {
            var warnings = await Service.GetWarningsAsAdmin(count);

            return View(warnings);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWarning(Guid warningId)
        {
            await Service.DeleteWarning(warningId);

            return RedirectToAction("GetWarningsAsAdmin");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateWarning(Guid warningId)
        {
            var warning = await Service.GetUpdateWarning(warningId);

            return View(warning);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateWarning(UpdateWarningModel model)
        {
            await Service.UpdateWaning(model);

            return RedirectToAction("GetWarningsAsAdmin");
        }

    }
}
