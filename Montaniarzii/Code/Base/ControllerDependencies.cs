using Microsoft.AspNetCore.Mvc.Rendering;
using Montaniarzii.Common.DTOs;

namespace Montaniarzii.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserDto CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDto currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
