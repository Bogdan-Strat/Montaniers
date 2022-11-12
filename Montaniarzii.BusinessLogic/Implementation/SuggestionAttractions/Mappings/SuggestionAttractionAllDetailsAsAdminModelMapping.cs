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
    public class SuggestionAttractionAllDetailsAsAdminModelMapping : Profile
    {
        public SuggestionAttractionAllDetailsAsAdminModelMapping()
        {
            CreateMap<SuggestionAttraction, SuggestionAttractionAllDetailsModelAsAdmin>()
                .ForMember(sa => sa.Username, o => o.MapFrom(m => m.User.Username));
        }
    }
}
