using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Attraction : IEntity
    {
        public Attraction()
        {
            AttractionXphotos = new HashSet<AttractionXphoto>();
            TripXattractions = new HashSet<TripXattraction>();
        }

        public Guid AttractionId { get; set; }
        public byte TypeAttractionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Location { get; set; }
        public int? Height { get; set; }
        public string Mountains { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TypeAttraction TypeAttraction { get; set; }
        public virtual ICollection<AttractionXphoto> AttractionXphotos { get; set; }
        public virtual ICollection<TripXattraction> TripXattractions { get; set; }
    }
}
