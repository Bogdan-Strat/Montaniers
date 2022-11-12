using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Models
{
    public class GetWarningModelAsUser
    {
        public string WarningMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
