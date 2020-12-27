using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(LeaveRequest entity)
        {
            await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveRequest>> FindAll()
        {
            var leaveHistories = await _db.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.ApprovedBy)
                .Include(x => x.LeaveType)
                .ToListAsync();
            return leaveHistories;
        }

        public async Task<LeaveRequest> FindById(int Id)
        {
            var leaveHistory = _db.LeaveRequests
                .Include(x => x.RequestingEmployee)
                .Include(x => x.ApprovedBy)
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.Id == Id);
            return await leaveHistory;
        }

        public async Task<ICollection<LeaveRequest>> GetLeaveRequestByEmployee(string employeeid)
        {
            var leaveRequests = await FindAll();
            return leaveRequests
                .Where(x => x.RequestingEmployeeId == employeeid)
                .ToList();
        }

        public async Task<bool> IsExists(int Id)
        {
            var exists = await _db.LeaveRequests.AnyAsync(x => x.Id == Id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }
    }
}