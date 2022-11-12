using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Photos.Validation
{
    public class PhotoValidation : AbstractValidator<IFormFile>
    {
        public PhotoValidation()
        {
            RuleFor(r => r.FileName)
                .Must(IsPhoto).When(model => model.Length > 0).WithMessage("Photo msut have .jpg, .png or .jpeg extesnion");
        }

        private bool IsPhoto(string photoName)
        {
            var extension = photoName.Split('.').LastOrDefault();
            var validExtensions = new List<string>() { "png", "jpg", "jpeg" };

            if (extension == null)
                return false;

            return validExtensions.Contains(extension);

        }
    }
}
