using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Invitations.Models;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class InvitationService : BaseService
    {
        public InvitationService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public async Task<List<InvitationModel>> getAllActiveInvitations()
        {
            return await UnitOfWork.Invitations
                .Get()
                .Include(i => i.Trip)
                .Where(i => i.UserId == CurrentUser.Id && i.AnswerDate == null && i.Trip.IsDeleted == false)
                .Select(i => new InvitationModel()
                {
                    TripId = i.TripId,
                    UsernameEventCreator = i.Trip.User.Username,
                    UserId = i.Trip.UserId
                })
                .ToListAsync();
        }

        public async Task<Invitation> GetInvitationByTripIdAndUsername(Guid tripId)
        {
            var invitation = await UnitOfWork.Invitations
                .Get()
                .Include(i => i.Trip)
                .Where(i => i.TripId == tripId && i.UserId == CurrentUser.Id && i.Trip.IsDeleted == false)
                .SingleOrDefaultAsync();

            if(invitation == null)
            {
                throw new NotFoundErrorException();
            }

            return invitation;
        }

        public async Task AcceptInvitation(Guid tripId)
        {
            await ExecuteInTransactionAsync(async unitOfWork =>
            {
                var invitation = await GetInvitationByTripIdAndUsername(tripId);

                if(invitation == null)
                {
                    throw new NotFoundErrorException();
                }

                //new List<int>() { 1,2,3}.Except(new List<int>() { 1, 2 })

                invitation.IsAccepted = true;
                invitation.AnswerDate = DateTime.Now;

                unitOfWork.Invitations.Update(invitation);
                await unitOfWork.SaveChangesAsync();

                var trip = await unitOfWork.Trips
                    .Get()
                    .SingleOrDefaultAsync(t => t.TripId == tripId && t.IsDeleted == false);

                if(trip == null)
                {
                    throw new NotFoundErrorException();
                }

                trip.ParticipationInTrips.Add(new ParticipationInTrip()
                {
                    TripId = tripId,
                    UserId = CurrentUser.Id,
                    ResponseDate = DateTime.Now
                });

                UnitOfWork.Trips.Update(trip);
                await unitOfWork.SaveChangesAsync();
            });
            

        }

        public async Task DeclineInvitation(Guid tripId)
        {
            var invitation = await GetInvitationByTripIdAndUsername(tripId);

            if(invitation == null)
            {
                throw new NotFoundErrorException();
            }

            invitation.IsAccepted = false;
            invitation.AnswerDate = DateTime.Now;

            UnitOfWork.Invitations.Update(invitation);
            await UnitOfWork.SaveChangesAsync();

        }
    }
}
