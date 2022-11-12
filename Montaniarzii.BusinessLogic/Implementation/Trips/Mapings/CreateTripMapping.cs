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
    public class CreateTripMapping : Profile
    {
        public CreateTripMapping()
        {
            CreateMap<CreateTripModel, Trip>()
                .ForMember(a => a.TypePublicityId, o => o.MapFrom(m => m.PrivacyId));
        }
    }
}
