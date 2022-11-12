using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class TripXphoto : IEntity
    {
        public Guid TripId { get; set; }
        public Guid PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
