using Montaniarzii.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Entities.Entities
{
    public partial class SuggestionAttraction : IEntity
    {
        public Guid SuggestionAttractionId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsApproved { get; set; }
        public string AttractionName { get; set; }
        public byte TypeAttractionId { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Location { get; set; }
        public int? Height { get; set; }
        public string Mountains { get; set; }

        public virtual TypeAttraction TypeAttraction { get; set; }
        public virtual User User { get; set; }
    }
}
