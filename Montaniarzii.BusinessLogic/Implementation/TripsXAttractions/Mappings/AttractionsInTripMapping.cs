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
    public class AttractionsInTripMapping : Profile
    {
        public AttractionsInTripMapping()
        {
            CreateMap<TripXattraction, AttractionsInTripModel>()
                .ForMember(txa => txa.MarkingName, o => o.MapFrom(m => (MarkingTypes)m.MarkingId))
                .ForMember(txa => txa.AttractionOrderNumber, o => o.MapFrom(m => m.OrderNumber))
                .ForMember(txa => txa.Latitude, o => o.MapFrom(m => m.Attraction.Latitude))
                .ForMember(txa => txa.Longitude, o => o.MapFrom(m => m.Attraction.Longitude));
        }
    }
}
