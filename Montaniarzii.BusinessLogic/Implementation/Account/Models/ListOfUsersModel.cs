using Montaniarzii.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Models
{
    public class ListOfUsersModel
    {
        public int NumberOfUsers { get; set; }
        public int ActualPageNumber { get; set; }
        public List<User> Users { get; set; }
        public ListOfUsersModel()
        {
            Users = new();
        }
    }
}
