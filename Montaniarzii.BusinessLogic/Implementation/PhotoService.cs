using Microsoft.AspNetCore.Http;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Photos.Validation;
using Montaniarzii.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class PhotoService : BaseService
    {
        private readonly PhotoValidation PhotoValidation;
        public PhotoService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            PhotoValidation = new PhotoValidation();
        }

        public async Task<Guid> SaveAndGetIdOfPhoto(IFormFile photo)
        {
            PhotoValidation.Validate(photo).ThenThrow(photo);


            var photoId = Guid.NewGuid();

            var dir = Path.Combine(AvatarFilePath, photoId.ToString());

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (photo.Length > 0)
            {
                string filePath = Path.Combine(dir, photo.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
            }

            return photoId;


        }

    }
}
