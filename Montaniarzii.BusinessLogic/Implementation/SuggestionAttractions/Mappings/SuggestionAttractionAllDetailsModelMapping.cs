using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Mappings
{
    public class SuggestionAttractionAllDetailsModelMapping : Profile
    {
        public SuggestionAttractionAllDetailsModelMapping()
        {
            CreateMap<SuggestionAttraction, SuggestionAttractionAllDetailsModel>()
                .ForMember(sa => sa.Username, o => o.MapFrom(m => m.User.Username))
                .ForMember(sa => sa.TypeAttraction, o => o.MapFrom(m => m.TypeAttraction.TypaAttractionName));
        }
    }
}
