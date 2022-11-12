using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Models
{
    public class GetWarningModelAsAdmin
    {
        public string UsernameAdmin { get; set; }
        public Guid WarningId { get; set; }
        public string WarningMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public int NumberOfWarnings { get; set; }
    }
}
