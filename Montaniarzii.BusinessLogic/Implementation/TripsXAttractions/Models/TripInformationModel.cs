using Montaniarzii.BusinessLogic.Implementation.Invitations.Models;
using Montaniarzii.Common.DTOs;
using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.TripsXAttractions.Models
{
    public class TripInformationModel
    {
        public Guid TripId { get; set; }
        public string Description { get; set; }
        public string Equipment { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public int Difficulty { get; set; }
        public string Privacy { get; set; }
        public string TypePost { get; set; }
        public List<AttractionsInTripModel> Attractions { get; set; }
        public List<InvitationInTripInformationModel> InvitedUsers { get; set; }
        public List<GuidSelectListItemModel<Photo>> Photos { get; set; }
        public TripInformationModel()
        {
            InvitedUsers = new();
            Attractions = new();
            Photos = new();
        }
    }
}
