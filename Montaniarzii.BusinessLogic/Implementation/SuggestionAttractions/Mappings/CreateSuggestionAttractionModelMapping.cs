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
    public class CreateSuggestionAttractionModelMapping : Profile
    {
        public CreateSuggestionAttractionModelMapping()
        {
            CreateMap<CreateSuggestionAttractionModel, SuggestionAttraction>()
                //.ForMember(sa => sa.Height, o=> o.MapFrom(m => int.Parse(m.Height)))
                .ForMember(sa => sa.Height, o => o.Ignore());
        }
    }
}
