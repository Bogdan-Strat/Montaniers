using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Invitations.Models;
using Montaniarzii.BusinessLogic.Implementation.TripsXAttractions.Models;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class TripXAttractionService : BaseService
    {
       // private readonly RegisterUserValidation RegisterUserValidator;
        public TripXAttractionService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
           // this.RegisterUserValidator = new RegisterUserValidation();
        }

        public async Task<TripInformationModel> GetTripInformation(Guid tripId)
        {
            var trip = await UnitOfWork.Trips
                .Get()
                .Include(t => t.TripXattractions)
                    .ThenInclude(a => a.Attraction)
                .Include(t => t.TripXphotos)
                    .ThenInclude(tp => tp.Photo)
                .SingleOrDefaultAsync(t => t.TripId == tripId && t.IsDeleted == false);

            if(trip == null)
            {
                throw new NotFoundErrorException();
            }

            var tripInformation = Mapper.Map<Trip, TripInformationModel>(trip);

            foreach(var attraction in trip.TripXattractions)
            {
                tripInformation.Attractions.Add(Mapper.Map<TripXattraction, AttractionsInTripModel>(attraction));
                tripInformation.Attractions.Last().AttractionName = (AttractionTypes)attraction.Attraction.TypeAttractionId + " " + attraction.Attraction.Name;
            }

            foreach(var photo in trip.TripXphotos)
            {
                tripInformation.Photos.Add(new GuidSelectListItemModel<Photo>()
                {
                    Id = photo.PhotoId,
                    Name = photo.Photo.Path
                });
            }

            
            return tripInformation;
        }

        public async Task<TripInformationModel> GetEventInformation(Guid tripId)
        {
            var trip = await UnitOfWork.Trips
                .Get()
                .Include(t => t.TripXattractions)
                    .ThenInclude(a => a.Attraction)
                .Include(t => t.Invitations)
                    .ThenInclude(t => t.User)
                .Include(t => t.TripXphotos)
                    .ThenInclude(tp => tp.Photo)
                .SingleOrDefaultAsync(t => t.TripId == tripId && t.IsDeleted == false);

            if(trip == null)
            {
                throw new NotFoundErrorException();
            }

            var eventInformation = Mapper.Map<Trip, TripInformationModel>(trip);

            foreach (var attraction in trip.TripXattractions)
            {
                eventInformation.Attractions.Add(Mapper.Map<TripXattraction, AttractionsInTripModel>(attraction));
                eventInformation.Attractions.Last().AttractionName = (AttractionTypes)attraction.Attraction.TypeAttractionId + " " + attraction.Attraction.Name;
            }

            foreach(var invitation in trip.Invitations)
            {
                if(invitation.User.Username != CurrentUser.Username)
                {
                    eventInformation.InvitedUsers.Add(new InvitationInTripInformationModel()
                    {
                        UserId = invitation.UserId,
                        Username = invitation.User.Username,
                        IsInvitationAccepted = await GetStatusInvitation(eventInformation.TripId, invitation.UserId)
                    });
                }
                
            }

            foreach (var photo in trip.TripXphotos)
            {
                eventInformation.Photos.Add(new GuidSelectListItemModel<Photo>()
                {
                    Id = photo.PhotoId,
                    Name = photo.Photo.Path
                });
            }

            return eventInformation;
        }

        public async Task<bool?> GetStatusInvitation(Guid tripId, Guid userId)
        {
            var status = await UnitOfWork.Invitations
                .Get()
                .Include(i => i.Trip)
                .Include(i => i.User)
                .Where(i => i.TripId == tripId && i.UserId == userId && i.Trip.IsDeleted == false && i.User.IsDeleted == false)
                .Select(i => new StatusDateModel()
                {
                    IsAccepted = i.IsAccepted,
                    AnswerDate = i.AnswerDate,
                })
                .SingleOrDefaultAsync();

            if (status == null)
            {
                throw new NotFoundErrorException();
            }

            if (status.AnswerDate == null)
                return null;
            else
            {
                return status.IsAccepted;
            }
        }

        public async Task<TripInformationModel> GetTripInformationForEdit(Guid tripId)
        {
            var trip = await UnitOfWork.Trips
                .Get()
                .Include(t => t.TripXattractions)
                    .ThenInclude(a => a.Attraction)
                .Include(t => t.TripXphotos)
                    .ThenInclude(tp => tp.Photo)
                .SingleOrDefaultAsync(t => t.TripId == tripId && t.IsDeleted == false);

            if (trip == null)
            {
                throw new NotFoundErrorException();
            }

            var tripInformation = Mapper.Map<Trip, TripInformationModel>(trip);

            foreach (var attraction in trip.TripXattractions)
            {
                tripInformation.Attractions.Add(Mapper.Map<TripXattraction, AttractionsInTripModel>(attraction));
                tripInformation.Attractions.Last().AttractionName = (AttractionTypes)attraction.Attraction.TypeAttractionId + " " + attraction.Attraction.Name;
            }

            //foreach (var photo in trip.TripXphotos)
            //{
            //    tripInformation.Photos.Add(new GuidSelectListItemModel<Photo>()
            //    {
            //        Id = photo.PhotoId,
            //        Name = photo.Photo.Path
            //    });
            //}


            return tripInformation;
        }

    }
}
