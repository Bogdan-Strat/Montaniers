using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class TypePost : IEntity
    {
        public TypePost()
        {
            Trips = new HashSet<Trip>();
        }

        public byte TypePostId { get; set; }
        public string TypePostName { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
