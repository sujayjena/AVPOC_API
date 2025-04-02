using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Persistence.Repositories
{
    public class ConfigRefRepository : GenericRepository, IConfigRefRepository
    {
        private IConfiguration _configuration;

        public ConfigRefRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveConfigRef(ConfigRef_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@Ref_Type", parameters.Ref_Type);
            queryParameters.Add("@Ref_Param", parameters.Ref_Param);
            queryParameters.Add("@Ref_Value1", parameters.Ref_Value1);
            queryParameters.Add("@Ref_Value2", parameters.Ref_Value2);
            queryParameters.Add("@Ref_Value3", parameters.Ref_Value3);
            queryParameters.Add("@Ref_Value4", parameters.Ref_Value4);
            queryParameters.Add("@Ref_Value5", parameters.Ref_Value5);
            queryParameters.Add("@Ref_Value6", parameters.Ref_Value6);
            queryParameters.Add("@Ref_Value7", parameters.Ref_Value7);
            queryParameters.Add("@Ref_Value8", parameters.Ref_Value8);
            queryParameters.Add("@Ref_Value9", parameters.Ref_Value9);
            queryParameters.Add("@Ref_Value10", parameters.Ref_Value10);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveConfigRef", queryParameters);
        }

        public async Task<IEnumerable<ConfigRef_Response>> GetConfigRefList(ConfigRef_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@Ref_Type", parameters.Ref_Type);
            queryParameters.Add("@Ref_Param", parameters.Ref_Param);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ConfigRef_Response>("GetConfigRefList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ConfigRef_Response?> GetConfigRefById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<ConfigRef_Response>("GetConfigRefById", queryParameters)).FirstOrDefault();
        }
    }
}
