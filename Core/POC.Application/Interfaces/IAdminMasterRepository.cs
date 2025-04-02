using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Interfaces
{
    public interface IAdminMasterRepository
    {
        Task<int> SaveGate(Gate_Request parameters);
        Task<IEnumerable<Gate_Response>> GetGateList(Gate_Search parameters);
        Task<Gate_Response?> GetGateById(long Id = 0);
    }
}
