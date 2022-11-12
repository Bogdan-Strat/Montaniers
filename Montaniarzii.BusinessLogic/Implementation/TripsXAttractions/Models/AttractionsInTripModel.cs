using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.TripsXAttractions.Models
{
    public class AttractionsInTripModel
    {
        public Guid AttractionId { get; set; }
        public string AttractionName { get; set; }
        public int Duration { get; set; }
        public string MarkingName { get; set; }
        public int AttractionOrderNumber { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
