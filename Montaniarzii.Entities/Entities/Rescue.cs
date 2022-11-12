using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Rescue : IEntity
    {
        public int RescueId { get; set; }
        public string RescueName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
