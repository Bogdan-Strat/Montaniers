using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Trip : IEntity
    {
        public Trip()
        {
            Invitations = new HashSet<Invitation>();
            ParticipationInTrips = new HashSet<ParticipationInTrip>();
            TripXattractions = new HashSet<TripXattraction>();
            TripXphotos = new HashSet<TripXphoto>();
            Likes = new HashSet<Like>();
        }

        public Guid TripId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public byte RatingId { get; set; }
        public byte DifficultyId { get; set; }
        public string Equipment { get; set; }
        public DateTime TripDate { get; set; }
        public byte TypePostId { get; set; }
        public byte TypePublicityId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Difficulty Difficulty { get; set; }
        public virtual Rating Rating { get; set; }
        public virtual TypePost TypePost { get; set; }
        public virtual TypePublicity TypePublicity { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }
        public virtual ICollection<ParticipationInTrip> ParticipationInTrips { get; set; }
        public virtual ICollection<TripXattraction> TripXattractions { get; set; }
        public virtual ICollection<TripXphoto> TripXphotos { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
