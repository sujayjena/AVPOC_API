using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Models
{
    public class Login_Request
    {
        public string? MobileNumber { get; set; }
        public string? Passwords { get; set; }
    }
    public class UsersLoginSessionData
    {
        public int? Id { get; set; }
        public string? SecurityName { get; set; }
        public string? SecurityMobileNo { get; set; }
        public bool IsActive { get; set; }
    }
}
