using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Models
{
    public class UpdateWarningModel
    {
        public Guid WarningId { get; set; }
        public string WarningMessage { get; set; }
        public DateTime EndDate { get; set; }
    }
}
