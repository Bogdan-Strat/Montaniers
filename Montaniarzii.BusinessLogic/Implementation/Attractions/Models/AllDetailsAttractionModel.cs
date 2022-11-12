using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Attractions.Models
{
    public class AllDetailsAttractionModel
    {
        public Guid AttractionId { get; set; }
        public string TypeAttraction { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Location { get; set; }
        public int? Height { get; set; }
        public string Mountains { get; set; }
    }
}
