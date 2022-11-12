using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class ParticipationInTrip : IEntity
    {
        public Guid UserId { get; set; }
        public Guid TripId { get; set; }
        public DateTime ResponseDate { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual User User { get; set; }
    }
}
