using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.Warnings.Models;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Mappings
{
    public class CreateWarningModelMapping : Profile
    {
        public CreateWarningModelMapping()
        {
            CreateMap<CreateWarningModel, Warning>();
        }
    }
}
