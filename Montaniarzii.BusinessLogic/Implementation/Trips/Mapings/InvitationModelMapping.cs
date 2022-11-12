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
    public class InvitationModelMapping : Profile
    {
        public InvitationModelMapping()
        {
            CreateMap<Guid, Invitation>()
                .ForMember(a => a.UserId, a => a.MapFrom(m => Guid.NewGuid()));
        }
    }
}
