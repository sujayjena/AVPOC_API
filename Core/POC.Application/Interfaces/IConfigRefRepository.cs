using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Interfaces
{
    public interface IConfigRefRepository
    {
        Task<int> SaveConfigRef(ConfigRef_Request parameters);

        Task<IEnumerable<ConfigRef_Response>> GetConfigRefList(ConfigRef_Search parameters);

        Task<ConfigRef_Response?> GetConfigRefById(int Id);
    }
}
