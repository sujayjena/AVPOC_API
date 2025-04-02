using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<int> SaveAttendance(Attendance_Request parameters);
        Task<IEnumerable<Attendance_Response>> GetAttendanceList(Attendance_Search parameters);
    }
}
