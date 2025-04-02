using POC.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Interfaces
{
    public interface IVisitorRepository
    {
        Task<int> SaveVisitors(Visitor_Request parameters);
        Task<IEnumerable<Visitor_Response>> GetVisitorsList(Visitor_Search parameters);
        Task<Visitor_Response?> GetVisitorsById(long Id = 0, string MobileNumber = "");
        Task<int> VisitorsApproveNReject(Visitor_ApproveNReject parameters);

        Task<int> SaveVisitorGate(VisitorGate_Request parameters);
        Task<IEnumerable<VisitorGate_Response>> GetVisitorGateById(int VisitorId, int GateId);

        Task<int> SaveBarcode(Barcode_Request parameters);
        Task<Barcode_Response?> GetBarcodeById(string BarcodeNo);
    }
}
