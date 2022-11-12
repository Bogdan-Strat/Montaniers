using Montaniarzii.BusinessLogic.Implementation.Trips.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Models
{
    public class UserProfileModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public bool? statusRequest { get; set; }
        public string AvatarPhotoPath { get; set; }
        public Guid AvatarPhotoId { get; set; }
        public List<TripXAttractionGetAllTripsModel> Trips { get; set; }
        
        public UserProfileModel()
        {
            Trips = new List<TripXAttractionGetAllTripsModel>();
        }
    }
}
