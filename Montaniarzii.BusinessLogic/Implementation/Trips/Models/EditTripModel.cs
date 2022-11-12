using Montaniarzii.Common.DTOs;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Models
{
    public class EditTripModel
    {
        public string Description { get; set; }
        public DateTime TripDate { get; set; }
        public byte RatingId { get; set; }
        public byte DifficultyId { get; set; }
        public string Equipment { get; set; }
        public byte PrivacyId { get; set; }
        public byte TypePostId { get; set; }
        //public Guid StartPointId { get; set; }
        public List<TripXAttractionModel> Attractions { get; set; }
        public List<Guid> UsersId { get; set; }
        public List<GuidSelectListItemModel<Photo>> Photos { get; set; }

        public EditTripModel()
        {
            UsersId = new();
            Attractions = new List<TripXAttractionModel>();
            Photos = new();
        }
    }
}
