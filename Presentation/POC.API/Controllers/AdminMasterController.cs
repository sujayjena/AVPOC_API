using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC.Application.Enums;
using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Application.Models;
using System.Net;
using System.Text;

namespace POC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMasterController : ControllerBase
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IAdminMasterRepository _adminMasterRepository;

        public AdminMasterController(IFileManager fileManager, IAdminMasterRepository adminMasterRepository)
        {
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
            _adminMasterRepository = adminMasterRepository;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveGate(Gate_Request parameters)
        {
            int result = await _adminMasterRepository.SaveGate(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved successfully";
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetGateList(Gate_Search parameters)
        {
            IEnumerable<Gate_Response> lstRoles = await _adminMasterRepository.GetGateList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetGateById(long Id = 0)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _adminMasterRepository.GetGateById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
