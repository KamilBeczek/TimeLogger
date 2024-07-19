using FirebirdSql.Data.Services;
using Microsoft.AspNetCore.Mvc;
using TimeLogger.Data.Repository.IRepository;
using TimeLogger.Models;

namespace TimeLogger.Controllers
{
    public class WorklogsController : Controller
    {
        private readonly IWorkLogsRepository _workLogRepository;

        public WorklogsController(IWorkLogsRepository workLogService)
        {
            _workLogRepository = workLogService;
        }

        public IActionResult Search(string query, DateTime? startDate, DateTime? endDate)
        {
            startDate ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate ??= DateTime.Now;

            var worklogs = _workLogRepository.GetWorklogs(startDate.Value, endDate.Value);

            var filteredWorklogs = worklogs
                .Where(w => 
                w.AUTHOR.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var totalHoursByCategory = filteredWorklogs
                .GroupBy(w => w.QUALIFICATION)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(w => w.TIMESPENT)
                );

            var model = new WorklogViewModel
            {
                ProductiveHours = totalHoursByCategory.GetValueOrDefault("RP", 0) + totalHoursByCategory.GetValueOrDefault("R", 0),
                HelpDeskHours = totalHoursByCategory.GetValueOrDefault("HD", 0),
                DevelopmentHours = totalHoursByCategory.GetValueOrDefault("SZ", 0),
                InternalHours = totalHoursByCategory.GetValueOrDefault("W", 0),
                Worklogs = filteredWorklogs
            };

            return PartialView("_WorklogsTablePartial", model);
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate, string username, string project)
        {
            DateTime defaultStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime defaultEndDate = DateTime.Now;

            startDate ??= defaultStartDate;
            endDate ??= defaultEndDate;

            var worklogs = _workLogRepository.GetWorklogs(startDate.Value, endDate.Value);

            if (!string.IsNullOrEmpty(username))
            {
                worklogs = worklogs.Where(w => w.AUTHOR.Contains(username, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(project))
            {
                worklogs = worklogs.Where(w => w.PROJECT.Contains(project, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalHoursByCategory = worklogs
                .GroupBy(w => w.QUALIFICATION)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(w => w.TIMESPENT)
                );

            var model = new WorklogViewModel
            {
                ProductiveHours = totalHoursByCategory.GetValueOrDefault("RP", 0) + totalHoursByCategory.GetValueOrDefault("R", 0),
                HelpDeskHours = totalHoursByCategory.GetValueOrDefault("HD", 0),
                DevelopmentHours = totalHoursByCategory.GetValueOrDefault("SZ", 0),
                InternalHours = totalHoursByCategory.GetValueOrDefault("W", 0),
                Worklogs = worklogs
            };

            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["Username"] = username;
            ViewData["PROJECT"] = project; 

            return View(model);
        }

    }
}
