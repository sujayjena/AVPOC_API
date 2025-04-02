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
    public class VisitorRepository : GenericRepository, IVisitorRepository
    {

        private IConfiguration _configuration;

        public VisitorRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


        public async Task<int> SaveVisitors(Visitor_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@VisitStartDate", parameters.VisitStartDate);
            queryParameters.Add("@VisitEndDate", parameters.VisitEndDate);
            queryParameters.Add("@VisitorName", parameters.VisitorName);
            queryParameters.Add("@VisitorMobileNo", parameters.VisitorMobileNo);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@ImageOriginalFileName", parameters.ImageOriginalFileName);
            queryParameters.Add("@ImageFileName", parameters.ImageFileName);
            queryParameters.Add("@IsActive", parameters.IsActive);

            return await SaveByStoredProcedure<int>("SaveVisitors", queryParameters);
        }

        public async Task<IEnumerable<Visitor_Response>> GetVisitorsList(Visitor_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);

            var result = await ListByStoredProcedure<Visitor_Response>("GetVisitorsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<Visitor_Response?> GetVisitorsById(long Id = 0, string MobileNumber = "")
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            queryParameters.Add("@MobileNumber", MobileNumber);
            return (await ListByStoredProcedure<Visitor_Response>("GetVisitorsById", queryParameters)).FirstOrDefault();
        }
        public async Task<int> VisitorsApproveNReject(Visitor_ApproveNReject parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@StatusId", parameters.StatusId);
            queryParameters.Add("@BarcodeOriginalFileName", parameters.BarcodeOriginalFileName);
            queryParameters.Add("@BarcodeFileName", parameters.BarcodeFileName);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("VisitorsApproveNReject", queryParameters);
        }
        public async Task<int> SaveVisitorGate(VisitorGate_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Action", parameters.Action);
            queryParameters.Add("@VisitorId", parameters.VisitorId);
            queryParameters.Add("@GateId", parameters.GateId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveVisitorGate", queryParameters);
        }

        public async Task<IEnumerable<VisitorGate_Response>> GetVisitorGateById(int VisitorId, int GateId)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@VisitorId", VisitorId);
            queryParameters.Add("@GateId", GateId);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<VisitorGate_Response>("GetVisitorGateById", queryParameters);

            return result;
        }

        public async Task<int> SaveBarcode(Barcode_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@BarcodeNo", parameters.BarcodeNo);
            queryParameters.Add("@BarcodeType", parameters.BarcodeType);
            queryParameters.Add("@Barcode_Unique_Id", parameters.Barcode_Unique_Id);
            queryParameters.Add("@RefId", parameters.RefId);

            return await SaveByStoredProcedure<int>("SaveBarcode", queryParameters);
        }

        public async Task<Barcode_Response?> GetBarcodeById(string BarcodeNo)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@BarcodeNo", BarcodeNo);

            return (await ListByStoredProcedure<Barcode_Response>("GetBarcodeById", queryParameters)).FirstOrDefault();
        }
    }
}
