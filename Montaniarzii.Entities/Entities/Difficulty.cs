using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Difficulty : IEntity
    {
        public Difficulty()
        {
            Trips = new HashSet<Trip>();
        }

        public byte DifficultyId { get; set; }
        public byte DifficultyScore { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
