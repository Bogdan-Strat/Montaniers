using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class AttractionXphoto : IEntity
    {
        public Guid AttractionId { get; set; }
        public Guid PhotoId { get; set; }

        public virtual Attraction Attraction { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
