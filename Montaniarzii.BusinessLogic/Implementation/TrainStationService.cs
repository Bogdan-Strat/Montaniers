using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.TrainStations.Models;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class TrainStationService : BaseService
    {
        public TrainStationService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }
        public async Task<List<TrainStationModel>> GetClosestTrainsStationsPaged(Guid attractionId, int count = 0)
        {
            var attraction = await UnitOfWork.Attractions
                .Get()
                .SingleOrDefaultAsync(a => a.AttractionId == attractionId && a.IsDeleted == false);

            if (attraction == null)
            {
                throw new NotFoundErrorException();
            }

            var attractionLongitude = attraction.Longitude;
            var attractionLatitude = attraction.Latitude;

            var trainStations = await UnitOfWork.TrainStations
                .Get()
                //.OrderBy(ts => Math.Abs(ts.Latitude - attractionLatitude) + Math.Abs(ts.Longitude - attractionLongitude))
                //.Take(50 * count)
                .Select(ts => Mapper.Map<TrainStation, TrainStationModel>(ts))
                .ToListAsync();

            const double radius = 6371; // Radius of the Earth in km.
            foreach (var trainStation in trainStations)
            {
                var lat1 = DegreesToRadians((double)attractionLatitude);
                var lon1 = DegreesToRadians((double)attractionLongitude);
                var lat2 = DegreesToRadians((double)trainStation.Latitude);
                var lon2 = DegreesToRadians((double)trainStation.Longitude);

                double d_lat = lat2 - lat1;
                double d_lon = lon2 - lon1;
                double h = Math.Sin(d_lat / 2) * Math.Sin(d_lat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(d_lon / 2) * Math.Sin(d_lon / 2);
                trainStation.Distance = 2 * radius * Math.Asin(Math.Sqrt(h));

            }

            return trainStations
                .OrderBy(ts => ts.Distance)
                .Skip(count * 5)
                .Take(5)
                .ToList();


        }
        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }

}
