using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Interfaces
{
    public interface ILoginRepository
    {
        Task<UsersLoginSessionData?> ValidateUserLoginByEmail(Login_Request parameters);
    }
}
