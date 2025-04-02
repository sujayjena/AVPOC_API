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
    public class AttendanceRepository : GenericRepository, IAttendanceRepository
    {

        private IConfiguration _configuration;

        public AttendanceRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }


        public async Task<int> SaveAttendance(Attendance_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@SecurityId", parameters.SecurityId);
            queryParameters.Add("@GateId", parameters.GateId);
            queryParameters.Add("@AttendanceStatus", parameters.AttendanceStatus);
            queryParameters.Add("@BatteryStatus", parameters.BatteryStatus);
            queryParameters.Add("@VisitorId", parameters.VisitorId);

            return await SaveByStoredProcedure<int>("SaveAttendance", queryParameters);
        }

        public async Task<IEnumerable<Attendance_Response>> GetAttendanceList(Attendance_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@EmployeeId", parameters.EmployeeId);
            queryParameters.Add("@GateId", parameters.GateId);
            queryParameters.Add("@VisitorId", parameters.VisitorId);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);

            var result = await ListByStoredProcedure<Attendance_Response>("GetAttendanceList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }
    }
}
