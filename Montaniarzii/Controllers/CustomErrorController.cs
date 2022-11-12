using Microsoft.AspNetCore.Mvc;
using Montaniarzii.Code.Base;

namespace Montaniarzii.Controllers
{
    public class CustomErrorController : BaseController
    {
        public CustomErrorController(ControllerDependencies dependencies) : base(dependencies)
        {
        }

        [HttpGet]
        public IActionResult Error_BadRequest()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error404()
        {
            return View();
        }
    }
}
