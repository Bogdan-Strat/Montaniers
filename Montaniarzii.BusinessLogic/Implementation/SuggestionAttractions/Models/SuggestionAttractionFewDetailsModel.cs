using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models
{
    public class SuggestionAttractionFewDetailsModel
    {
        public Guid SuggestionAttractionId { get; set; }
        public bool? IsAccepted { get; set; }
        public string AttractionName { get; set; }
        public string TypeAttraction { get; set; }
        public DateTime CretedDate { get; set; }
    }
}
