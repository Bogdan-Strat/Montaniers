using Microsoft.AspNetCore.Mvc;
using Montaniarzii.Common.DTOs;

namespace Montaniarzii.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentUserDto CurrentUser;

        public BaseController(ControllerDependencies dependencies)
            : base()
        {
            CurrentUser = dependencies.CurrentUser;
        }
    }
}
