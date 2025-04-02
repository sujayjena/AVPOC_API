using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC.Application.Enums;
using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Application.Models;

namespace POC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private ResponseModel _response;
        private readonly IAttendanceRepository _attendanceRepository;
        private IFileManager _fileManager;

        public AttendanceController(IAttendanceRepository attendanceRepository, IFileManager fileManager)
        {
            _attendanceRepository = attendanceRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveAttendance(Attendance_Request parameters)
        {
            int result = await _attendanceRepository.SaveAttendance(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetAttendanceList(Attendance_Search parameters)
        {
            IEnumerable<Attendance_Response> lstUsers = await _attendanceRepository.GetAttendanceList(parameters);
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }
    }
}
