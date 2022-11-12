using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Follow : IEntity
    {
        public Guid FollowingUserId { get; set; }
        public Guid FollowedUserId { get; set; }
        public bool? IsAccepted { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual User FollowedUser { get; set; }
        public virtual User FollowingUser { get; set; }
    }
}
