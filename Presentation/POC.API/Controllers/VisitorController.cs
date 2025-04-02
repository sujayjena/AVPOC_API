using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POC.Application.Enums;
using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Application.Models;
using POC.Persistence.Repositories;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace POC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IVisitorRepository _visitorRepository;
        private readonly IAdminMasterRepository _adminMasterRepository;

        public VisitorController(IFileManager fileManager, IVisitorRepository visitorRepository, IAdminMasterRepository adminMasterRepository)
        {
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
            _visitorRepository = visitorRepository;
            _adminMasterRepository = adminMasterRepository;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveVisitors(Visitor_Request parameters)
        {
            // Image Upload
            if (parameters != null && !string.IsNullOrWhiteSpace(parameters.Image_base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.Image_base64, "\\Uploads\\Visitors\\", parameters.ImageOriginalFileName);
                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.ImageFileName = vUploadFile;
                }
            }

            int result = await _visitorRepository.SaveVisitors(parameters);

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

                #region // Add/Update Gate

                // Delete Old Gate

                var vGateDELETEObj = new VisitorGate_Request()
                {
                    Action = "DELETE",
                    VisitorId = result,
                    GateId = 0
                };
                int resultGateDELETE = await _visitorRepository.SaveVisitorGate(vGateDELETEObj);


                // Add new Gate Deatils
                foreach (var vitem in parameters.GateList)
                {
                    var vGateDObj = new VisitorGate_Request()
                    {
                        Action = "INSERT",
                        VisitorId = result,
                        GateId = vitem.GateId
                    };

                    int resultGateD = await _visitorRepository.SaveVisitorGate(vGateDObj);
                }

                #endregion
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVisitorsList(Visitor_Search parameters)
        {
            IEnumerable<Visitor_Response> lstUsers = await _visitorRepository.GetVisitorsList(parameters);
            foreach (var user in lstUsers)
            {
                var vGateDetailsObj = await _visitorRepository.GetVisitorGateById(user.Id, 0);

                foreach (var item in vGateDetailsObj)
                {
                    var vGateObj = await _adminMasterRepository.GetGateById(Convert.ToInt32(item.GateId));
                    var vGateResOnj = new VisitorGate_Response()
                    {
                        Id = item.Id,
                        VisitorId = user.Id,
                        GateId = item.GateId,
                        GateNumber = vGateObj != null ? vGateObj.GateNumber : string.Empty,
                    };

                    user.GateList.Add(vGateResOnj);
                }
            }

            _response.Data = lstUsers.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVisitorsById(long Id = 0,string? MobileNumber = "")
        {
            if (Id <= 0 && MobileNumber == "")
            {
                _response.Message = "Id or Mobile Number is required";
            }
            else
            {
                var vResultObj = await _visitorRepository.GetVisitorsById(Id, MobileNumber);
                if (vResultObj != null)
                {
                    var vGateDetailsObj = await _visitorRepository.GetVisitorGateById(vResultObj.Id, 0);

                    foreach (var item in vGateDetailsObj)
                    {
                        var vGateObj = await _adminMasterRepository.GetGateById(Convert.ToInt32(item.GateId));
                        var vGateResOnj = new VisitorGate_Response()
                        {
                            Id = item.Id,
                            VisitorId = vResultObj.Id,
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> VisitorsApproveNReject(Visitor_ApproveNReject parameters)
        {
            if (parameters.Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                if (parameters.StatusId == 2)
                {
                    var vVisitorResponse = await _visitorRepository.GetVisitorsById(Convert.ToInt32(parameters.Id));
                    if (vVisitorResponse != null)
                    {
                        //Prepare you post parameters  
                        var postData = new Visitor_Barcode_Request()
                        {
                            value = vVisitorResponse.VisitNumber
                        };

                        //Call API
                        string sendUri = "http://164.52.213.175:5050/generate_barcode_v2";

                        //Create HTTPWebrequest  
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendUri);

                        var jsonData = JsonConvert.SerializeObject(postData);

                        //Prepare and Add URL Encoded data  
                        UTF8Encoding encoding = new UTF8Encoding();
                        byte[] data = encoding.GetBytes(jsonData);

                        //Specify post method  
                        httpWReq.Method = "POST";
                        httpWReq.ContentType = "application/json";
                        httpWReq.ContentLength = data.Length;
                        using (Stream stream = httpWReq.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }

                        //Get the response  
                        HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string responseString = reader.ReadToEnd();

                        //Close the response  
                        reader.Close();

                        response.Close();

                        dynamic jsonResults = JsonConvert.DeserializeObject<dynamic>(responseString);
                        var status = jsonResults.ContainsKey("isSuccess") ? jsonResults.isSuccess : false;

                        if (status == true)
                        {
                            var barcode = jsonResults["barcode"];

                            var barcode_image_base64 = barcode.ContainsKey("barcode_image_base64") ? barcode.barcode_image_base64 : string.Empty;
                            var vbarcode_image_base64 = Convert.ToString(barcode_image_base64);

                            var unique_id = barcode.ContainsKey("unique_id") ? barcode.unique_id : string.Empty;
                            var vUniqueId = Convert.ToString(unique_id);

                            if (!string.IsNullOrWhiteSpace(vbarcode_image_base64))
                            {
                                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(vbarcode_image_base64, "\\Uploads\\Barcode\\", vUniqueId + ".png");
                                if (!string.IsNullOrWhiteSpace(vUploadFile))
                                {
                                    parameters.BarcodeOriginalFileName = vUniqueId + ".png";
                                    parameters.BarcodeFileName = vUploadFile;
                                }
                            }

                            if (vUniqueId != "")
                            {
                                var vBarcode_Request = new Barcode_Request()
                                {
                                    Id = 0,
                                    BarcodeNo = vVisitorResponse.VisitNumber,
                                    BarcodeType = "Visitor",
                                    Barcode_Unique_Id = vUniqueId,
                                    RefId = vVisitorResponse.Id
                                };
                                var resultBarcode = _visitorRepository.SaveBarcode(vBarcode_Request);
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(parameters.BarcodeFileName) && parameters.StatusId == 2)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Barcode is not generated";

                    return _response;
                }

                int resultExpenseDetails = await _visitorRepository.VisitorsApproveNReject(parameters);

                if (resultExpenseDetails == (int)SaveOperationEnums.NoRecordExists)
                {
                    _response.Message = "No record exists";
                }
                else if (resultExpenseDetails == (int)SaveOperationEnums.ReocrdExists)
                {
                    _response.Message = "Record already exists";
                }
                else if (resultExpenseDetails == (int)SaveOperationEnums.NoResult)
                {
                    _response.Message = "Something went wrong, please try again";
                }
                else
                {

                    _response.Message = "Record details saved sucessfully";
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBarcodeById(string BarcodeNo)
        {
            if (BarcodeNo == "")
            {
                _response.Message = "Barcode No. is required";
            }
            else
            {
                var vResultObj = await _visitorRepository.GetBarcodeById(BarcodeNo);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
