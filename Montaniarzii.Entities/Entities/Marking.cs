using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Marking : IEntity
    {
        public Marking()
        {
            TripXattractions = new HashSet<TripXattraction>();
        }

        public byte MarkingId { get; set; }
        public string MarkingName { get; set; }

        public virtual ICollection<TripXattraction> TripXattractions { get; set; }
    }
}
