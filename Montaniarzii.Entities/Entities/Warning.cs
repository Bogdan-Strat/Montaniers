using Montaniarzii.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Entities.Entities
{
    public partial class Warning : IEntity
    {
        public Guid WarningId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string WarningMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User User { get; set; }
    }
}
