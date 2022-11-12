using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Models
{
    public class TripXAttractionGetAllTripsModel
    {
        public Guid UserId { get; set; }
        public Guid TripId { get; set; }
        public List<string> AttractionsName { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public string UserNameCreator { get; set; }
        public string privacy { get; set; }
        public string PostType { get; set; }
        public TripXAttractionGetAllTripsModel()
        {
            AttractionsName = new();
        }
    }
}
