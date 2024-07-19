using TimeLogger.Models;

namespace TimeLogger.Data.Repository.IRepository
{
    public interface IWorkLogsRepository
    {
        public Task<List<X_JIRA_WORKLOG>> GetAllWorklogsAsync();
        public IEnumerable<X_JIRA_WORKLOG> GetWorklogs(DateTime startDate, DateTime endDate);
    }
}
