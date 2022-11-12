using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.SuggestionAttractions.Models
{
    public class GetSuggestionsPagedModel
    {
        public int NumberOfSuggestions { get; set; }
        public int ActualPageNumber { get; set; }
        public List<SuggestionAttractionAllDetailsModelAsAdmin> ListOfSuggestions { get; set; }

        public GetSuggestionsPagedModel()
        {
            ListOfSuggestions = new();
        }
    }
}
