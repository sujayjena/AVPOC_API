using POC.Domain.Entities;
using POC.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Models
{
    public class Gate_Search : BaseSearchEntity
    {
    }

    public class Gate_Request : BaseEntity
    {
        public string? GateNumber { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Gate_Response : BaseResponseEntity
    {
        public string? GateNumber { get; set; }
        public bool? IsActive { get; set; }
    }
}
