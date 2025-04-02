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
    public class ManageSecurityController : ControllerBase
    {
        private ResponseModel _response;
        private readonly IManageSecurityRepository _manageSecurityRepository;
        private readonly IAdminMasterRepository _adminMasterRepository;
        private IFileManager _fileManager;

        public ManageSecurityController(IManageSecurityRepository manageSecurityRepository, IFileManager fileManager, IAdminMasterRepository adminMasterRepository)
        {
            _manageSecurityRepository = manageSecurityRepository;
            _fileManager = fileManager;
            _adminMasterRepository = adminMasterRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Security 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSecurity(Security_Request parameters)
        {
            int result = await _manageSecurityRepository.SaveSecurity(parameters);

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

                #region // Add/Update Gate

                // Delete Old Gate

                var vGateDELETEObj = new SecurityGate_Request()
                {
                    Action = "DELETE",
                    SecurityId = result,
                    GateId = 0
                };
                int resultGateDELETE = await _manageSecurityRepository.SaveSecurityGate(vGateDELETEObj);


                // Add new Gate Deatils
                foreach (var vitem in parameters.GateList)
                {
                    var vGateDObj = new SecurityGate_Request()
                    {
                        Action = "INSERT",
                        SecurityId = result,
                        GateId = vitem.GateId
                    };

                    int resultGateD = await _manageSecurityRepository.SaveSecurityGate(vGateDObj);
                }

                #endregion
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSecurityList(Security_Search parameters)
        {
            IEnumerable<Security_Response> lstUsers = await _manageSecurityRepository.GetSecurityList(parameters);
            if (lstUsers != null)
            {
                foreach (var user in lstUsers)
                {
                    var vGateDetailsObj = await _manageSecurityRepository.GetSecurityGateById(user.Id, 0);

                    foreach (var item in vGateDetailsObj)
                    {
                        var vGateObj = await _adminMasterRepository.GetGateById(Convert.ToInt32(item.GateId));
                        var vGateResOnj = new SecurityGate_Response()
                        {
                            Id = item.Id,
                            SecurityId = user.Id,
                            GateId = item.GateId,
                            GateNumber = vGateObj != null ? vGateObj.GateNumber : string.Empty,
                        };

                        user.GateList.Add(vGateResOnj);
                    }
                }
            }
            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSecurityById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageSecurityRepository.GetSecurityById(Id);

                if (vResultObj != null)
                {
                    var vGateDetailsObj = await _manageSecurityRepository.GetSecurityGateById(vResultObj.Id, 0);

                    foreach (var item in vGateDetailsObj)
                    {
                        var vGateObj = await _adminMasterRepository.GetGateById(Convert.ToInt32(item.GateId));
                        var vGateResOnj = new SecurityGate_Response()
                        {
                            Id = item.Id,
                            SecurityId = vResultObj.Id,
                            GateId = item.GateId,
                            GateNumber = vGateObj != null ? vGateObj.GateNumber : string.Empty,
                        };

                        vResultObj.GateList.Add(vGateResOnj);
                    }
                }
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
