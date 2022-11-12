using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Models
{
    public class TripXAttractionModel
    {
        public Guid AttractionId { get; set; }
        public byte MarkingId { get; set; }
        public decimal Duration { get; set; }
    }
}
