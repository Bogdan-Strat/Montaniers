using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Warnings.Models
{
    public class ListOfWarningAsAdminModel
    {
        public int NumberOfWarnings { get; set; }
        public int ActualPageNumber { get; set; }
        public List<GetWarningModelAsAdmin> ListOfWarnings { get; set; }

        public ListOfWarningAsAdminModel()
        {
            ListOfWarnings = new();
        }
    }
}
