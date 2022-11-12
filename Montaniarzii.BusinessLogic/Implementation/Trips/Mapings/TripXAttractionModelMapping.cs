using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.Trips.Models;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Mapings
{
    public class TripXAttractionModelMapping : Profile
    {
        public TripXAttractionModelMapping()
        {
            CreateMap<TripXAttractionModel, TripXattraction>()
                .ForMember(a => a.TripId, a => a.MapFrom(s => Guid.NewGuid()));
        }
    }
}
