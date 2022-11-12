using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class LikeService : BaseService
    {
        public LikeService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public async Task LikeTrip(Guid tripId)
        {
            var trip = await UnitOfWork.Trips
                .Get()
                .Where(t => t.TripId == tripId)
                .SingleOrDefaultAsync();

            if(trip == null)
            {
                throw new NotFoundErrorException();
            }

            UnitOfWork.Likes.Insert(new Like()
            {
                TripId = tripId,
                UserId = CurrentUser.Id
            });

            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsTripLikedByCurrentUser(Guid tripId)
        {
            var like = await UnitOfWork.Likes
                .Get()
                .Where(l => l.TripId == tripId && l.UserId == CurrentUser.Id)
                .SingleOrDefaultAsync();

            if(like == null)
            {
                return false;
            }

            return true;
        }

        public async Task<int> GetNumberOfLikesForATrip(Guid tripId)
        {
            return await UnitOfWork.Likes
                .Get()
                .Where(l => l.TripId == tripId)
                .CountAsync();
        }

        public async Task DislikeTrip(Guid tripId)
        {
            var like = await UnitOfWork.Likes
                .Get()
                .Where(l => l.TripId == tripId && l.UserId == CurrentUser.Id)
                .SingleOrDefaultAsync();

            UnitOfWork.Likes.HardDelete(like);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
