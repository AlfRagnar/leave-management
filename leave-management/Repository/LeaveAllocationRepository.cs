using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .Where(x => x.EmployeeId == employeeid && x.LeaveTypeId == leavetypeid && x.Period == period)
                .Any();
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            var AllLeaveAllocation = _db.LeaveAllocations.ToList();
            return AllLeaveAllocation;
        }

        public LeaveAllocation FindById(int Id)
        {
            var leaveAllocation = _db.LeaveAllocations.Find(Id);
            return leaveAllocation;
        }

        public bool isExists(int Id)
        {
            var exists = _db.LeaveAllocations.Any(x => x.Id == Id);
            return exists;
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}