using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations
                .Where(x => x.EmployeeId == employeeid && x.LeaveTypeId == leavetypeid && x.Period == period)
                .Any();
        }

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            var AllLeaveAllocation = await _db.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .ToListAsync();
            return AllLeaveAllocation;
        }

        public async Task<LeaveAllocation> FindById(int Id)
        {
            var leaveAllocation = await _db.LeaveAllocations
                .Include(x => x.LeaveType)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == Id);
            return leaveAllocation;
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            var findAll = await FindAll();
            return findAll.Where(x => x.EmployeeId == employeeid && x.Period == period).ToList();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByEmployeeAndType(string employeeid, int leavetypeid)
        {
            var period = DateTime.Now.Year;
            var findAll = await FindAll();
            return findAll.FirstOrDefault(x => x.EmployeeId == employeeid && x.Period == period && x.LeaveTypeId == leavetypeid);
        }

        public async Task<bool> IsExists(int Id)
        {
            var exists = await _db.LeaveAllocations.AnyAsync(x => x.Id == Id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}