using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Invitations.Models
{
    public class InvitationModel
    {
        public Guid UserId { get; set; }
        public string UsernameEventCreator { get; set; }
        public Guid TripId { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime AnswerDate { get; set; }
    }
}
