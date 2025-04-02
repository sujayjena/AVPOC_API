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
    public class AdminMasterRepository : GenericRepository, IAdminMasterRepository
    {

        private IConfiguration _configuration;

        public AdminMasterRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


        public async Task<int> SaveGate(Gate_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@GateNumber", parameters.GateNumber);
            queryParameters.Add("@IsActive", parameters.IsActive);

            return await SaveByStoredProcedure<int>("SaveGate", queryParameters);
        }

        public async Task<IEnumerable<Gate_Response>> GetGateList(Gate_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);

            var result = await ListByStoredProcedure<Gate_Response>("GetGateList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Gate_Response?> GetGateById(long Id = 0)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<Gate_Response>("GetGateById", queryParameters)).FirstOrDefault();
        }
    }
}

