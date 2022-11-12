using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Attractions.Models
{
    public class FewDetailsAttractionModel
    {
        public Guid AttractionId { get; set; }
        public string AttractionName { get; set; }
        public string AttractionType { get; set; }
        public int? Height { get; set; }
        public string? Location { get; set; }
        public string? Mountains { get; set; }
        public int Count { get; set; }
    }
}
