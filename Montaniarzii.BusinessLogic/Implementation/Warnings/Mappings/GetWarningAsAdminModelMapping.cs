using AutoMapper;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Mappings
{
    public class GetWarningAsAdminModelMapping : Profile
    {
        public GetWarningAsAdminModelMapping()
        {
            CreateMap<Warning, GetWarningAsAdminModel>();
        }
    }
}
