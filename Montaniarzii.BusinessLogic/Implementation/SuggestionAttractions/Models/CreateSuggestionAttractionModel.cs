using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models
{
    public class CreateSuggestionAttractionModel
    {
        public string AttractionName { get; set; }
        public byte TypeAttractionId { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Location { get; set; }
        public string Height { get; set; }
        public string Mountains { get; set; }
    }
}
