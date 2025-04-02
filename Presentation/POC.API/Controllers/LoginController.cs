using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POC.Application.Constants;
using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Application.Models;
using POC.Domain.Entities;

namespace POC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ResponseModel _response;
        private ILoginRepository _loginRepository;
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
           
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ResponseModel> Login(Login_Request parameters)
        {
            (string, DateTime) tokenResponse;
            UsersLoginSessionData? loginResponse;

            parameters.Passwords = EncryptDecryptHelper.EncryptString(parameters.Passwords);

            loginResponse = await _loginRepository.ValidateUserLoginByEmail(parameters);

            if (loginResponse != null)
            {
                if (loginResponse.IsActive == true)
                {
                    //tokenResponse = _jwt.GenerateJwtToken(loginResponse);

                    //if (loginResponse.UserId != null)
                    //{
                    //    string strBrnachIdList = string.Empty;

                    //    var vRoleList = await _rolePermissionRepository.GetRoleMasterEmployeePermissionById(Convert.ToInt64(loginResponse.UserId));
                    //    //var vUserNotificationList = await _notificationService.GetNotificationListById(Convert.ToInt64(loginResponse.EmployeeId));
                    //    var vUserDetail = await _userRepository.GetUserById(Convert.ToInt32(loginResponse.UserId));
                    //    var vUserBranchMappingDetail = await _branchRepository.GetBranchMappingByEmployeeId(EmployeeId: Convert.ToInt32(loginResponse.UserId), BranchId: 0);
                    //    if (vUserBranchMappingDetail.ToList().Count > 0)
                    //    {
                    //        strBrnachIdList = string.Join(",", vUserBranchMappingDetail.ToList().OrderBy(x => x.BranchId).Select(x => x.BranchId));
                    //    }

                    //    employeeSessionData = new SessionDataEmployee
                    //    {
                    //        UserId = loginResponse.UserId,
                    //        UserCode = loginResponse.UserCode,
                    //        UserName = loginResponse.UserName,
                    //        MobileNumber = loginResponse.MobileNumber,
                    //        EmailId = loginResponse.EmailId,
                    //        UserType = loginResponse.UserType,
                    //        RoleId = loginResponse.RoleId,
                    //        RoleName = loginResponse.RoleName,
                    //        IsMobileUser = loginResponse.IsMobileUser,
                    //        IsWebUser = loginResponse.IsWebUser,
                    //        IsActive = loginResponse.IsActive,
                    //        Token = tokenResponse.Item1,

                    //        CompanyId = vUserDetail != null ? Convert.ToInt32(vUserDetail.CompanyId) : 0,
                    //        CompanyName = vUserDetail != null ? vUserDetail.CompanyName : String.Empty,
                    //        DepartmentId = vUserDetail != null ? Convert.ToInt32(vUserDetail.DepartmentId) : 0,
                    //        DepartmentName = vUserDetail != null ? vUserDetail.DepartmentName : String.Empty,
                    //        BranchId = strBrnachIdList,

                    //        ProfileImage = vUserDetail != null ? vUserDetail.ProfileImage : String.Empty,
                    //        ProfileOriginalFileName = vUserDetail != null ? vUserDetail.ProfileOriginalFileName : String.Empty,
                    //        ProfileImageURL = vUserDetail != null ? vUserDetail.ProfileImageURL : String.Empty,

                    //        UserRoleList = vRoleList.ToList(),
                    //        //UserNotificationList = vUserNotificationList.ToList()
                    //    };

                    //    //_response.Data = loginResponse;
                    //}

                    ////Login History
                    //loginHistoryParameters = new UserLoginHistorySaveParameters
                    //{
                    //    UserId = loginResponse.UserId,
                    //    UserToken = tokenResponse.Item1,
                    //    IsLoggedIn = true,
                    //    IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    //    DeviceName = HttpContext.Request.Headers["User-Agent"],
                    //    TokenExpireOn = tokenResponse.Item2,
                    //    RememberMe = parameters.Remember
                    //};

                    //await _loginRepository.SaveUserLoginHistory(loginHistoryParameters);

                    _response.Data = loginResponse;
                    _response.Message = MessageConstants.LoginSuccessful;
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = ErrorConstants.InactiveProfileError;
                }
            }
            else
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid credential, please try again with correct credential";
            }

            return _response;
        }
    }
}
