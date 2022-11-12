using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Common.DTOs
{
    public class CurrentUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Role { get; set; }
        public string PhotoId { get; set; }
        public string PhotoPath { get; set; }

        public static implicit operator Guid(CurrentUserDto v)
        {
            throw new NotImplementedException();
        }
    }
}
