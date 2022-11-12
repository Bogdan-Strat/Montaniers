using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.Code.Base;
using Montaniarzii.Common.Exceptions;

namespace Montaniarzii.Controllers
{
    [Authorize]
    public class PhotoController : BaseController
    {
        private readonly PhotoService Service;
        public PhotoController(ControllerDependencies dependencies, PhotoService service) : base(dependencies)
        {
            Service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAndGetIdOfPhoto([FromForm(Name = "file")] IFormFile photo)
        {
            if (photo == null)
            {
                throw new BadRequestErrorException();
            }
            //var extension = photo.FileName.Split('.').LastOrDefault();
            //if (extension == null)
            //{
            //    throw new BadRequestErrorException();
            //}

            //var validExtensions = new List<string>() { "jpg", "png", "jpeg" };
            //if (!(validExtensions.Contains(extension)))
            //{
            //    throw new BadRequestErrorException();
            //}

            var photoId = await Service.SaveAndGetIdOfPhoto(photo);

            return Ok(photoId);
        }
    }
}
