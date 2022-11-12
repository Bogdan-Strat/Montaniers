using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.TripsXAttractions.Models;
using Montaniarzii.Entities.Entities;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.TripsXAttractions.Mappings
{
    public class TripInformationMapping : Profile
    {
        public TripInformationMapping()
        {
            CreateMap<Trip, TripInformationModel>()
                .ForMember(t => t.Difficulty, o => o.MapFrom(m => m.DifficultyId))
                .ForMember(t => t.Rating, o => o.MapFrom(m => m.RatingId))
                .ForMember(t => t.Privacy, o => o.MapFrom(m => (PublicityTypes)m.TypePublicityId))
                .ForMember(t => t.TypePost, o => o.MapFrom(m=> (PostTypes)m.TypePostId))
                .ForMember(t => t.Date, o => o.MapFrom(m => m.TripDate));
                
        }
    }
}
