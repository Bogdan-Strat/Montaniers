using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.BusinessLogic.Implementation.Account.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool AreCredentialsInvalid { get; set; }
    }
}
