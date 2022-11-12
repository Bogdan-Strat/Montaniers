using Microsoft.EntityFrameworkCore;
using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation.Follows.Models;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Common.Exceptions;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation
{
    public class FollowService : BaseService
    {
        public FollowService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public async Task RequestFollow(RequestFollowModel model)
        {
            var followRequest = Mapper.Map<RequestFollowModel, Follow>(model);

            followRequest.ModifiedDate = DateTime.Now;
            followRequest.IsAccepted = false;

            UnitOfWork.Follows.Insert(followRequest);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool?> GetStatusRequest(Guid followedUserId)
        {
            var statusRequest = await UnitOfWork.Follows
                .Get()
                .Where(f => f.FollowedUserId == followedUserId & f.FollowingUserId == CurrentUser.Id)
                .Select(f => f.IsAccepted)
                .SingleOrDefaultAsync();

            return statusRequest;
        } 

        public async Task<List<PendingFollowModel>> GetAllPendingFollowsRequests()
        {
            var followRequests =  await UnitOfWork.Follows
                .Get()
                .Include(f => f.FollowingUser)
                .Where(f => f.FollowedUserId == CurrentUser.Id && f.IsAccepted == false)
                .Select(f => new PendingFollowModel()
                {
                    FollowingUserId  = f.FollowingUserId,
                    ModifiedDate = f.ModifiedDate,
                    FollowingUsername = f.FollowingUser.Username
                })
                .ToListAsync();

            return followRequests;
        }

        public async Task ApproveFollowRequest(Guid followingUserId)
        {
            var followRequest = await UnitOfWork.Follows
                .Get()
                .Where(f => f.FollowingUserId == followingUserId && f.FollowedUserId == CurrentUser.Id)
                .SingleOrDefaultAsync();

            if(followRequest == null)
            {
                throw new NotFoundErrorException();
            }

            followRequest.IsAccepted = true;
            followRequest.ModifiedDate = DateTime.Now;

            UnitOfWork.Follows.Update(followRequest);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DenyFollowRequest(Guid followingUserId)
        {
            var followRequest = await UnitOfWork.Follows
                .Get()
                .Where(f => f.FollowingUserId == followingUserId && f.FollowedUserId == CurrentUser.Id)
                .SingleOrDefaultAsync();

            if (followRequest == null)
            {
                throw new NotFoundErrorException();
            }

            UnitOfWork.Follows.HardDelete(followRequest);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<GuidSelectListItemModel<Follow>>> GetPeopleThatYouFollow()
        {
            var peopleThatYouFollow = await UnitOfWork.Follows
                .Get()
                .Where(f => f.IsAccepted == true && f.FollowingUserId == CurrentUser.Id)
                .OrderBy(f => f.FollowingUser.Username)
                .Select(f => new GuidSelectListItemModel<Follow>()
                {
                    Id = f.FollowedUserId,
                    Name = f.FollowedUser.Username
                })
                .ToListAsync();

            return peopleThatYouFollow;
        }

        public async Task<List<Guid>> GetIdOfPeopleYouFollow()
        {
            var ids = await UnitOfWork.Follows
                .Get()
                .Where(f => f.IsAccepted == true && f.FollowingUserId == CurrentUser.Id)
                .Select(f => f.FollowedUserId)
                .ToListAsync();

            return ids;
        }

        public async Task<List<GuidSelectListItemModel<Follow>>> GetPeopleThatFollowYou()
        {
            var peopleThatFollowYou = await UnitOfWork.Follows
                .Get()
                .Where(f => f.IsAccepted == true && f.FollowedUserId == CurrentUser.Id)
                .OrderBy(f => f.FollowingUser.Username)
                .Select(f => new GuidSelectListItemModel<Follow>()
                {
                    Id = f.FollowingUserId,
                    Name = f.FollowingUser.Username
                })
                .ToListAsync();

            return peopleThatFollowYou;
        }

        public async Task Unfollow(Guid followeduserId)
        {
            if (followeduserId == Guid.Empty)
            {
                throw new NotFoundErrorException();
            }


            var followRequest = await UnitOfWork.Follows
                .Get()
                .Where(f => f.FollowedUserId == followeduserId && f.FollowingUserId == CurrentUser.Id)
                .SingleOrDefaultAsync();

            UnitOfWork.Follows.HardDelete(followRequest);
            await UnitOfWork.SaveChangesAsync();
        }
    }
}
