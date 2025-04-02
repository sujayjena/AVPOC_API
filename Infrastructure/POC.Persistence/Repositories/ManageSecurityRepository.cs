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
    public class ManageSecurityRepository: GenericRepository, IManageSecurityRepository
    {
        private IConfiguration _configuration;

        public ManageSecurityRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveSecurity(Security_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@SecurityName", parameters.SecurityName);
            queryParameters.Add("@SecurityMobileNo", parameters.SecurityMobileNo);
            queryParameters.Add("@Passwords", !string.IsNullOrWhiteSpace(parameters.Passwords) ? EncryptDecryptHelper.EncryptString(parameters.Passwords) : string.Empty);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveSecurity", queryParameters);
        }

        public async Task<IEnumerable<Security_Response>> GetSecurityList(Security_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<Security_Response>("GetSecurityList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Security_Response?> GetSecurityById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Security_Response>("GetSecurityById", queryParameters)).FirstOrDefault();
        }

        public async Task<int> SaveSecurityGate(SecurityGate_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Action", parameters.Action);
            queryParameters.Add("@SecurityId", parameters.SecurityId);
            queryParameters.Add("@GateId", parameters.GateId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveSecurityGate", queryParameters);
        }

        public async Task<IEnumerable<SecurityGate_Response>> GetSecurityGateById(int SecurityId, int GateId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SecurityId", SecurityId);
            queryParameters.Add("@GateId", GateId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<SecurityGate_Response>("GetSecurityGateById", queryParameters);

            return result;
        }
    }
}