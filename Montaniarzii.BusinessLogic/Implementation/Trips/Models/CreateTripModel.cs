using Microsoft.AspNetCore.Http;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Entities.Entities;
using Montaniarzii.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Trips.Models
{
    public class CreateTripModel
    {
        public string Description { get; set; }
        public DateTime TripDate { get; set; } = DateTime.Now.Date;
        public byte RatingId { get; set; }
        public byte DifficultyId { get; set; }
        public string Equipment { get; set; }
        public byte PrivacyId { get; set; }
        public byte TypePostId { get; set; }
        //public Guid StartPointId { get; set; }
        public List<TripXAttractionModel> Attractions { get; set; }
        public List<Guid> UsersId { get; set; }
        public List<GuidSelectListItemModel<Photo>> Photos { get; set; }

        public CreateTripModel()
        {
            UsersId = new();
            Attractions = new List<TripXAttractionModel>();
            Photos = new();
        }
    }
}
