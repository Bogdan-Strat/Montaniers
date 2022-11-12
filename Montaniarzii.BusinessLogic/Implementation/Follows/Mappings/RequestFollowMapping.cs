using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.Follows.Models;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Follows.Mappings
{
    public class RequestFollowMapping : Profile
    {
        public RequestFollowMapping()
        {
            CreateMap<RequestFollowModel, Follow>();
        }
    }
}
