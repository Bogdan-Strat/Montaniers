using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class TripXattraction : IEntity
    {
        public Guid AttractionId { get; set; }
        public Guid TripId { get; set; }
        public int OrderNumber { get; set; }
        public byte MarkingId { get; set; }
        public int Duration { get; set; }

        public virtual Attraction Attraction { get; set; }
        public virtual Marking Marking { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
