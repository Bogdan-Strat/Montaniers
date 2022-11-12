using Montaniarzii.BusinessLogic.Implementation.Account.Models;
using Montaniarzii.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Mappings
{
    public class EditUserProfile : Profile
    {
        public EditUserProfile()
        {
            CreateMap<EditUserModel, User>();

        }
    }
}
