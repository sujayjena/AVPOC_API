using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Interfaces
{
    public interface IManageSecurityRepository
    {
        Task<int> SaveSecurity(Security_Request parameters);
        Task<IEnumerable<Security_Response>> GetSecurityList(Security_Search parameters);
        Task<Security_Response?> GetSecurityById(int Id);

        Task<int> SaveSecurityGate(SecurityGate_Request parameters);
        Task<IEnumerable<SecurityGate_Response>> GetSecurityGateById(int SecurityId, int GateId);
    }
}
