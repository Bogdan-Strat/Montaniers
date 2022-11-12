using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class InvitationController : BaseController
    {
        private readonly InvitationService Service;
        public InvitationController(ControllerDependencies dependencies, InvitationService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingInvitations()
        {
            var invitations = await Service.getAllActiveInvitations();

            return View(invitations);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPendingInvitationsForJS()
        {
            var invitations = await Service.getAllActiveInvitations();

            return Ok(invitations);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptInvitation(Guid tripId)
        {
            await Service.AcceptInvitation(tripId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeclineInvitation(Guid tripId)
        {
            await Service.DeclineInvitation(tripId);

            return Ok();
        }
    }
}
