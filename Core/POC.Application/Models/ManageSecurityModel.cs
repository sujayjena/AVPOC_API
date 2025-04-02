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
    public class Security_Search : BaseSearchEntity
    {
    }

    public class Security_Request : BaseEntity
    {
        public Security_Request()
        {
            GateList = new List<SecurityGate_Request>();
        }
        public string? SecurityName { get; set; }
        public string? SecurityMobileNo { get; set; }
        public string? Passwords { get; set; }
        public bool? IsActive { get; set; }
        public List<SecurityGate_Request>? GateList { get; set; }
    }

    public class Security_Response : BaseResponseEntity
    {
        public Security_Response()
        {
            GateList = new List<SecurityGate_Response>();
        }
        public string? SecurityName { get; set; }
        public string? SecurityMobileNo { get; set; }
        public bool? IsActive { get; set; }
        public List<SecurityGate_Response>? GateList { get; set; }
    }

    public class SecurityGate_Request : BaseEntity
    {
        [JsonIgnore]
        public string? Action { get; set; }

        [JsonIgnore]
        public int? SecurityId { get; set; }
        public int GateId { get; set; }
    }

    public class SecurityGate_Response : BaseEntity
    {
        public int? SecurityId { get; set; }
        public int? GateId { get; set; }
        public string? GateNumber { get; set; }
    }
}
