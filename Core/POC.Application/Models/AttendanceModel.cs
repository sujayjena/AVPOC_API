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
    public class Attendance_Search : BaseSearchEntity
    {
        public int? EmployeeId { get; set; }

        [DefaultValue("")]
        public string? GateId { get; set; }
    }

    public class Attendance_Request : BaseEntity
    {
        public int? SecurityId { get; set; }
        public int? GateId { get; set; }
        public string? AttendanceStatus { get; set; }
        public string? BatteryStatus { get; set; }
        public int? VisitorId { get; set; }
    }

    public class Attendance_Response : BaseResponseEntity
    {
        public int? SecurityId { get; set; }
        public string? SecurityName { get; set; }
        public int? GateId { get; set; }
        public string? GateNumber { get; set; }
        public string? AttendanceStatus { get; set; }
        public string? BatteryStatus { get; set; }
        public int? VisitorId { get; set; }
        public string? VisitorName { get; set; }
    }
}
