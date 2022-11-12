using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.TrainStations.Models
{
    public class TrainStationModel
    {
        public Guid TrainStationId { get; set; }
        public string TrainStationName { get; set; }
        public string Location { get; set; }
        public double Distance { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
