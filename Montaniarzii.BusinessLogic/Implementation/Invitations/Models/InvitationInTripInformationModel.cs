using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Invitations.Models
{
    public class InvitationInTripInformationModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public bool? IsInvitationAccepted { get; set; }
    }
}
