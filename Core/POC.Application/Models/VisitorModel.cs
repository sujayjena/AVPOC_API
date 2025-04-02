using POC.Domain.Entities;
using POC.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POC.Application.Models
{
    public class Visitor_Search : BaseSearchEntity
    {
        public int? StatusId { get; set; }
    }

    public class Visitor_Request : BaseEntity
    {
        public Visitor_Request()
        {
            GateList = new List<SecurityGate_Request>();
        }
        public DateTime? VisitStartDate { get; set; }
        public DateTime? VisitEndDate { get; set; }
        public string? VisitorName { get; set; }
        public string? VisitorMobileNo { get; set; }
        public int? StatusId { get; set; }

        [DefaultValue("")]
        public string? ImageOriginalFileName { get; set; }

        [JsonIgnore]
        public string? ImageFileName { get; set; }

        [DefaultValue("")]
        public string? Image_base64 { get; set; }
        public bool? IsActive { get; set; }
        public List<SecurityGate_Request>? GateList { get; set; }
    }

    public class Visitor_Response : BaseResponseEntity
    {
        public Visitor_Response()
        {
            GateList = new List<VisitorGate_Response>();
        }
        public string? VisitNumber { get; set; }
        public DateTime? VisitStartDate { get; set; }
        public DateTime? VisitEndDate { get; set; }
        public string? VisitorName { get; set; }
        public string? VisitorMobileNo { get; set; }
        public int? StatusId { get; set; }
        public string? BarcodeOriginalFileName { get; set; }
        public string? BarcodeFileName { get; set; }
        public string? BarcodeURL { get; set; }
        public string? ImageOriginalFileName { get; set; }
        public string? ImageFileName { get; set; }
        public string? ImageURL { get; set; }
        public bool? IsActive { get; set; }
        public List<VisitorGate_Response>? GateList { get; set; }
    }

    public class Visitor_ApproveNReject
    {
        public int? Id { get; set; }
        public int? StatusId { get; set; }

        [JsonIgnore]
        public string? BarcodeOriginalFileName { get; set; }

        [JsonIgnore]
        public string? BarcodeFileName { get; set; }
    }

    public class Visitor_Barcode_Request
    {
        public string? value { get; set; }
    }

    public class VisitorGate_Request : BaseEntity
    {
        [JsonIgnore]
        public string? Action { get; set; }

        [JsonIgnore]
        public int? VisitorId { get; set; }
        public int GateId { get; set; }
    }

    public class VisitorGate_Response : BaseEntity
    {
        public int? VisitorId { get; set; }
        public int? GateId { get; set; }
        public string? GateNumber { get; set; }
    }

    public class Barcode_Request : BaseEntity
    {
        public string? BarcodeNo { get; set; }
        public string? BarcodeType { get; set; }
        public string? Barcode_Unique_Id { get; set; }
        public int? RefId { get; set; }
    }

    public class Barcode_Response : BaseEntity
    {
        public string? BarcodeNo { get; set; }
        public string? BarcodeType { get; set; }
        public string? Barcode_Unique_Id { get; set; }
        public int? RefId { get; set; }
        public string? Validity { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
