using Montaniarzii.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Like : IEntity
    {
        public Guid TripId { get; set; }
        public Guid UserId { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual User User { get; set; }
    }
}
