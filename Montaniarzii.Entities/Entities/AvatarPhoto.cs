using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class AvatarPhoto : IEntity
    {
        public Guid UserId { get; set; }
        public Guid PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual User User { get; set; }
    }
}
