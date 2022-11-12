using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.Attractions.Models;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Attractions.Mappings
{
    public class AllDetailsAttractionMappingModel : Profile
    {
        public AllDetailsAttractionMappingModel()
        {
            CreateMap<Attraction, AllDetailsAttractionModel>()
                .ForMember(a => a.TypeAttraction, o => o.MapFrom(m => m.TypeAttraction.TypaAttractionName));
        }
    }
}
