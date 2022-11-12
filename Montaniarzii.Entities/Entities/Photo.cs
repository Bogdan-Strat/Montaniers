using Montaniarzii.Common;
using System;
using System.Collections.Generic;

#nullable disable

namespace Montaniarzii.Entities.Entities
{
    public partial class Photo : IEntity
    {
        public Photo()
        {
            AttractionXphotos = new HashSet<AttractionXphoto>();
            AvatarPhotos = new HashSet<AvatarPhoto>();
            TripXphotos = new HashSet<TripXphoto>();
        }

        public Guid PhotoId { get; set; }
        public string Path { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<AttractionXphoto> AttractionXphotos { get; set; }
        public virtual ICollection<AvatarPhoto> AvatarPhotos { get; set; }
        public virtual ICollection<TripXphoto> TripXphotos { get; set; }
    }
}
