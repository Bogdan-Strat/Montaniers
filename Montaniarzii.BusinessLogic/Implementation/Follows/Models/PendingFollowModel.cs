using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Follows.Models
{
    public class PendingFollowModel
    {
        public Guid FollowingUserId { get; set; }
        public string FollowingUsername { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
