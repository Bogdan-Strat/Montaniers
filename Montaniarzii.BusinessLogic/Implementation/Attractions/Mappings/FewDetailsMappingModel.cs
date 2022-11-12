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
    public class FewDetailsMappingModel : Profile
    {
        public FewDetailsMappingModel()
        {
            CreateMap<Attraction, FewDetailsAttractionModel>()
                .ForMember(a => a.AttractionName, o => o.MapFrom(m => m.Name))
                .ForMember(a => a.AttractionType, o => o.MapFrom(m => m.TypeAttraction.TypaAttractionName));
        }
    }
}
