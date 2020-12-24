using leave_management.Data;
using System.Collections;
using System.Collections.Generic;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool CheckAllocation(int leavetypeid, string employeeid);
        ICollection<LeaveAllocation> GetLeaveAllocationByEmployee(string employeeid);
        LeaveAllocation GetLeaveAllocationByEmployeeAndType(string employeeid, int leavetypeid);
    }
}