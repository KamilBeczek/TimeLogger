using Microsoft.EntityFrameworkCore;
using TimeLogger.Data.Repository.IRepository;
using TimeLogger.Models;

namespace TimeLogger.Data.Repository
{
    public class WorkLogsRepository : Repository<X_JIRA_WORKLOG>, IWorkLogsRepository
    {
        private AppDbContext _db;

        public WorkLogsRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<X_JIRA_WORKLOG>> GetAllWorklogsAsync()
        {
            return await _db.X_JIRA_WORKLOG.ToListAsync();
        }

        public IEnumerable<X_JIRA_WORKLOG> GetWorklogs(DateTime startDate, DateTime endDate)
        {
            return _db.X_JIRA_WORKLOG
                           .Where(w => w.WORKLOGDATE >= startDate && w.WORKLOGDATE <= endDate)
                           .ToList();
        }

        public IEnumerable<IGrouping<string, X_JIRA_WORKLOG>> GetWorklogsByQualification(DateTime startDate, DateTime endDate)
        {
            var worklogs = GetWorklogs(startDate, endDate);
            return worklogs.GroupBy(w => w.QUALIFICATION);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(X_JIRA_WORKLOG obj)
        {
            _db.X_JIRA_WORKLOG.Update(obj);
        }
    }
}
