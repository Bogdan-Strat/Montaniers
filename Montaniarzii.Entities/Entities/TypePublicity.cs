using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class TypePublicity : IEntity
    {
        public TypePublicity()
        {
            Trips = new HashSet<Trip>();
        }

        public byte TypePublicityId { get; set; }
        public string TypePublicityName { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
