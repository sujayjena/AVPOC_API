using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Application.Enums
{
    public enum TicketStatusEnums
    {
        New = 1,
        AllocatedToTechnicalSupport = 2,
        PendingToAllocatedEngineer = 3,
        AllocatedToServiceEngineer = 4,
        AllocatedToServiceEngineer1 = 5,
        AllocatedToServiceEngineer2 = 6,
        ReferToTRC = 7,
        IntransitToTRC = 8,
        RecivedInTRC = 9,
        AllocatedToTRCEngineer = 10,
        PendingForParts = 11,
        TRCResolved = 12,
        PDI = 13,
        AssignBackToTRC = 14,
        IntransitToCustomer = 15,
        Resolved = 16,
        CSAT = 17,
        ReOpen = 18,
        Hold = 19,
        Resume = 20,
        Closed = 21
    }
}
