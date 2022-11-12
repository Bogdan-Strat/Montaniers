using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class TypeAttraction : IEntity
    {
        public TypeAttraction()
        {
            Attractions = new HashSet<Attraction>();
        }

        public byte TypeAttractionId { get; set; }
        public string TypaAttractionName { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }
        public virtual ICollection<SuggestionAttraction> SuggestionAttractions { get; set; }
    }
}
