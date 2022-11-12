using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Follows.Models
{
    public class RequestFollowModel
    {
        public Guid FollowingUserId { get; set; }
        public Guid FollowedUserId { get; set; }
    }
}
