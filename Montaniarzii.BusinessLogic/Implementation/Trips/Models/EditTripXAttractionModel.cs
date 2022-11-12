using Montaniarzii.Common.DTOs;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Models
{
    public class EditTripXAttractionModel
    {
        public Guid AttractionId { get; set; }
        public List<ByteSelectListItemModel<Marking>> Marking { get; set; }
        public int Duration { get; set; }

        public EditTripXAttractionModel()
        {
            Marking = new();
        }
    }
}
