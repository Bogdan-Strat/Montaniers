using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Montaniarzii.Entities.Entities;
using Montaniarzii.BusinessLogic.Implementation.Account.Models;

namespace Montaniarzii.BusinessLogic.Implementation.Account
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(a => a.UserId, a => a.MapFrom(s => Guid.NewGuid()));
        }

        
    }
}
