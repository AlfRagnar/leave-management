using leave_management.Data;
using System.Collections;
using System.Collections.Generic;

namespace leave_management.Contracts
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        ICollection<LeaveRequest> GetLeaveRequestByEmployee(string employeeid);
    }
}