using AutoMapper;
using Montaniarzii.BusinessLogic.Implementation.TrainStations.Models;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.TrainStations.Mappings
{
    public class TrainsStationModelMapping : Profile
    {
        public TrainsStationModelMapping()
        {
            CreateMap<TrainStation, TrainStationModel>()
                .ForMember(a => a.TrainStationName, o => o.MapFrom(m => m.Name));
        }
    }
}
