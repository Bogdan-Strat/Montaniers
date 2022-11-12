using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Trips.Models;
using Montaniarzii.BusinessLogic.Implementation.Trips.Validation;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Common.Extensions;
using Montaniarzii.Entities.Entities;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class TripService : BaseService
    {
        private readonly CreateTripValidation CreateTripValidator;
        public TripService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
            this.CreateTripValidator = new CreateTripValidation(UnitOfWork);
        }

        public async Task CreateTrip(CreateTripModel model)
        {
            await ExecuteInTransactionAsync(async unitOfWork =>
            {
                (await CreateTripValidator.ValidateAsync(model)).ThenThrow(model);


                var trip = Mapper.Map<CreateTripModel, Trip>(model);

                if (trip.TypePostId == 2)
                {
                    trip.TypePublicityId = 2;
                }
                trip.IsDeleted = false;
                trip.UserId = CurrentUser.Id;
                trip.CreateDate = DateTime.Now;
                trip.TripId = Guid.NewGuid();

                trip.TripXattractions = model.Attractions
                    .Select(ta => Mapper.Map<TripXAttractionModel, TripXattraction>(ta)).ToList();


                int i = 1;
                foreach (var attraction in trip.TripXattractions)
                {
                    if (attraction.AttractionId == Guid.Empty)
                    {
                        throw new NotFoundErrorException();
                    }
                    attraction.OrderNumber = i++;
                    attraction.TripId = trip.TripId;
                }

                trip.Invitations = model.UsersId
                    .Select(i => Mapper.Map<Guid, Invitation>(i))
                    .ToList();

                i = 0;
                foreach (var invitation in trip.Invitations)
                {
                    invitation.TripId = trip.TripId;
                    invitation.IsAccepted = false;
                    invitation.UserId = model.UsersId[i++];
                    //invitation.AnswerDate = null;
                }

                if(model.Photos.Count > 0)
                {
                    await SaveTripPhotos(model.Photos);

                    trip.TripXphotos = model.Photos
                        .Select(p => new TripXphoto()
                        {
                            PhotoId = p.Id,
                            TripId = trip.TripId
                        })
                        .ToList();
                }
                


                unitOfWork.Trips.Insert(trip);
                await unitOfWork.SaveChangesAsync();
            });
           
        }

        public async Task SaveTripPhotos(List<GuidSelectListItemModel<Photo>> photos)
        {
            for(var i = 0; i < photos.Count; i++)
            {
                UnitOfWork.Photos.Insert(new Photo()
                {
                    PhotoId = photos[i].Id,
                    Path = photos[i].Name,
                    IsDeleted = false
                });
                await UnitOfWork.SaveChangesAsync();
            }
        }

        //public async Task<EditTripModel> EditTrip(Guid tripId)
        //{
        //    var trip = await UnitOfWork.Trips
        //        .Get()
        //        .Include(p => p.TripXattractions)
        //        .SingleOrDefaultAsync(t => t.TripId == tripId && t.IsDeleted == false);

        //    if(trip == null)
        //    {
        //        throw new NotFoundErrorException();
        //    }

        //    return trip;
        //}

        private async Task<List<Trip>> GetAllTrips(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new NotFoundErrorException();
            }

            var trips = await UnitOfWork.Trips
                .Get()
                .Include(t => t.TripXattractions)
                    .ThenInclude(txa => txa.Attraction)
                .Include(t => t.Invitations)
                .Include(t => t.User)
                .Include(t => t.TypePublicity)
                .Include(t => t.TypePost)
                .Where(t => t.UserId == userId && t.IsDeleted == false)
                .OrderByDescending(t => t.CreateDate)
                .ToListAsync();

            return trips;
        }

        private async Task<List<Trip>> GetAllTripsForAListOfUsers(List<Guid> usersId, int count = 0)
        {
            var trips =  await UnitOfWork.Trips
                 .Get()
                 .Include(t => t.TripXattractions)
                     .ThenInclude(txa => txa.Attraction)
                 .Include(t => t.Invitations)
                 .Include(t => t.User)
                 .Include(t => t.TypePublicity)
                 .Include(t => t.TypePost)
                 .Where(t => usersId.Contains(t.UserId) && t.TypePublicity.TypePublicityName != "Private" && t.IsDeleted == false)
                 .OrderByDescending(t => t.CreateDate)
                 .Skip(count * 6)
                 .Take(6)
                 .ToListAsync();

            return trips;
        }

        private List<TripXAttractionGetAllTripsModel> TransformTripInModel(List<Trip> trips)
        {
            var listOfModels = new List<TripXAttractionGetAllTripsModel>();

            foreach (var trip in trips)
            {
                var model = new TripXAttractionGetAllTripsModel();
                var totalDuration = 0;
                foreach (var attraction in trip.TripXattractions)
                {
                    totalDuration += attraction.Duration;
                }

                var countAttractions = trip.TripXattractions.Count();

                var firstAttractionName = trip.TripXattractions
                    .Where(x => x.OrderNumber == 1)
                    .Select(fa => fa.Attraction.Name)
                    .SingleOrDefault();

                if(firstAttractionName == null)
                {
                    throw new NotFoundErrorException();
                }

                var lastAttractionName = trip.TripXattractions
                    .Where(x => x.OrderNumber == countAttractions)
                    .Select(fa => fa.Attraction.Name)
                    .SingleOrDefault();

                if(lastAttractionName == null)
                {
                    throw new NotFoundErrorException();
                }

                model.TripId = trip.TripId;
                model.Duration = totalDuration;
                model.Date = trip.CreateDate;
                model.AttractionsName.Add(firstAttractionName);
                model.AttractionsName.Add(lastAttractionName);
                model.UserId = trip.UserId;
                model.UserNameCreator = trip.User.Username;
                model.privacy = trip.TypePublicity.TypePublicityName;
                model.PostType = trip.TypePost.TypePostName;

                listOfModels.Add(model);

            }

            return listOfModels;
        }

        public async Task<List<TripXAttractionGetAllTripsModel>> GetAllTripsView(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new NotFoundErrorException();
            }

            var trips = await GetAllTrips(userId);

            return TransformTripInModel(trips);
        } 

        public async Task<List<TripXAttractionGetAllTripsModel>> GetAllTripsViewForAListOfUsers(List<Guid> usersId, int count = 0)
        {
            var trips = await GetAllTripsForAListOfUsers(usersId, count);

            return TransformTripInModel(trips);
        }

        public async Task DeleteTrip(Guid tripId)
        {
            var trip = await UnitOfWork.Trips
                .Get()
                .Where(t => t.IsDeleted == false && t.TripId == tripId)
                .SingleOrDefaultAsync();

            if(trip == null)
            {
                throw new NotFoundErrorException();
            }

            trip.IsDeleted = true;

            UnitOfWork.Trips.Update(trip);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<TripXAttractionGetAllTripsModel>> GetUpcomingEvents()
        {
            var events = await UnitOfWork.ParticipationInTrips
                .Get()
                .Include(pit => pit.Trip)
                    .ThenInclude(t => t.TripXattractions)
                        .ThenInclude(txa => txa.Attraction)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.Invitations)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.User)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.TypePublicity)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.TypePost)
                .Where(pit => pit.UserId == CurrentUser.Id && pit.Trip.IsDeleted == false && pit.Trip.TripDate > DateTime.Now)
                .OrderBy(pit => pit.Trip.TripDate)
                .Take(3)
                .Select(pit => pit.Trip)
                .ToListAsync();

            return TransformTripInModel(events);
        }

        public async Task<List<TripXAttractionGetAllTripsModel>> GetPastEvents()
        {
            var events = await UnitOfWork.ParticipationInTrips
                .Get()
                .Include(pit => pit.Trip)
                    .ThenInclude(t => t.TripXattractions)
                        .ThenInclude(txa => txa.Attraction)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.Invitations)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.User)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.TypePublicity)
                 .Include(pit => pit.Trip)
                    .ThenInclude(t => t.TypePost)
                .Where(pit => pit.UserId == CurrentUser.Id && pit.Trip.IsDeleted == false && pit.Trip.TripDate <= DateTime.Now)
                .OrderByDescending(pit => pit.Trip.TripDate)
                .Take(3)
                .Select(pit => pit.Trip)
                .ToListAsync();

            return TransformTripInModel(events);
        }

        

    }
}
