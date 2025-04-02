using Dapper;
using Microsoft.Extensions.Configuration;
using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Persistence.Repositories
{
    public class LoginRepository : GenericRepository, ILoginRepository
    {
        private IConfiguration _configuration;

        public LoginRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<UsersLoginSessionData?> ValidateUserLoginByEmail(Login_Request parameters)
        {
            IEnumerable<UsersLoginSessionData> lstResponse;
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Username", parameters.MobileNumber.SanitizeValue());
            queryParameters.Add("@Passwords", parameters.Passwords.SanitizeValue());

            lstResponse = await ListByStoredProcedure<UsersLoginSessionData>("ValidateUserLoginByUsername", queryParameters);
            return lstResponse.FirstOrDefault();
        }
    }
}
