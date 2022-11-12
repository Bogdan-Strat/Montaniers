using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class User : IEntity
    {
        public User()
        {
            AvatarPhotos = new HashSet<AvatarPhoto>();
            FollowFollowedUsers = new HashSet<Follow>();
            FollowFollowingUsers = new HashSet<Follow>();
            Invitations = new HashSet<Invitation>();
            ParticipationInTrips = new HashSet<ParticipationInTrip>();
            Trips = new HashSet<Trip>();
            Likes = new HashSet<Like>();
        }

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string HashedPassword { get; set; }
        public byte RoleId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<AvatarPhoto> AvatarPhotos { get; set; }
        public virtual ICollection<Follow> FollowFollowedUsers { get; set; }
        public virtual ICollection<Follow> FollowFollowingUsers { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<ParticipationInTrip> ParticipationInTrips { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Warning> Warnings { get; set; }
        public virtual ICollection<SuggestionAttraction> SuggestionAttractions { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
