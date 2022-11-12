using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Rating : IEntity
    {
        public Rating()
        {
            Trips = new HashSet<Trip>();
        }

        public byte RatingId { get; set; }
        public byte RatingScore { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
