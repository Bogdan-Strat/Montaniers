using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class TrainStation : IEntity
    {
        public Guid TrainStationId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }
    }
}
