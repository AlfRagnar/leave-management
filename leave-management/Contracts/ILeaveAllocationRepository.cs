using leave_management.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        Task<bool> CheckAllocation(int leavetypeid, string employeeid);

        Task<ICollection<LeaveAllocation>> GetLeaveAllocationByEmployee(string employeeid);

        Task<LeaveAllocation> GetLeaveAllocationByEmployeeAndType(string employeeid, int leavetypeid);
    }
}